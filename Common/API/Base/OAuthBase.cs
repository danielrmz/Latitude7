using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common7.Models.Google.OAuth;

using Hammock;
using Hammock.Web;
using Hammock.Authentication.OAuth;

using Common7.Models;
using Common7.Models.Google.Common;
using Common7.Models.Google.Latitude;

using Latitude7.API.Storage;
using Latitude7.API.Exceptions;

namespace Latitude7.API.Base
{
    public class OAuthBase
    {
        #region Properties

        private string ConsumerKey { get; set; }
        private string ConsumerSecret { get; set; }

        private AccessToken AccessToken { get; set; }

        private string RequestTokenUrl { get; set; }
        private string AccessTokenUrl { get; set; }
        private string AuthorizationUrl { get; set; }
        private string CallbackUrl { get; set; }

        /// <summary>
        /// Identifier of the current client session.  
        /// </summary>
        private Guid SessionId { get; set; }

        /// <summary>
        /// Rest Client helper
        /// </summary>
        private Hammock.RestClient Client { get; set;}
       
        /// <summary>
        /// Defines if the OAuth client state is on authenticated. 
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Storage container for tokens
        /// </summary>
        private static IStorageContainer<Token> _sessionStorage;
        public static IStorageContainer<Token> SessionStorage
        {
            get
            {
                _sessionStorage = _sessionStorage ?? new DefaultStorageContainer<Token>();
                return _sessionStorage;
            }

            set
            {
                _sessionStorage = value;
            }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Instantiates a client which is not yet authenticated
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="callbackUrl"></param>
        /// <param name="requestTokenUrl"></param>
        /// <param name="accessTokenUrl"></param>
        /// <param name="authorizationUrl"></param>
        public OAuthBase(Guid session_id, string consumerKey, string consumerSecret, Uri callbackUrl, string requestTokenUrl, 
                        string accessTokenUrl, string authorizationUrl)
        { 
            ConsumerKey      = consumerKey;
            ConsumerSecret   = consumerSecret;
            RequestTokenUrl  = requestTokenUrl;
            AccessTokenUrl   = accessTokenUrl;
            AuthorizationUrl = authorizationUrl;
            CallbackUrl      = callbackUrl.ToString();
            SessionId        = session_id;
            IsAuthenticated  = false;
            
            Client = new RestClient();
        }

        /// <summary>
        /// Instantiates a client which has already access to the app
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="accessKey"></param>
        /// <param name="accessSecret"></param>
        /// <param name="requestTokenUrl"></param>
        /// <param name="accessTokenUrl"></param>
        /// <param name="authorizationUrl"></param>
        public OAuthBase(Guid session_id, string consumerKey, string consumerSecret, string accessKey, string accessSecret, string requestTokenUrl,
                        string accessTokenUrl, string authorizationUrl)
        {
            ConsumerKey      = consumerKey;
            ConsumerSecret   = consumerSecret;
            RequestTokenUrl  = requestTokenUrl;
            AccessTokenUrl   = accessTokenUrl;
            AuthorizationUrl = authorizationUrl;
            AccessToken      = new AccessToken(accessKey, accessSecret);
            SessionId        = session_id;
            IsAuthenticated  = true;

            this.SetupClient(OAuthType.ProtectedResource);
        }

        /// <summary>
        /// Constructor when the login process has been done, the session_id will retrieve
        /// automatically the Access Token & Secret
        /// </summary>
        /// <param name="session_id"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        public OAuthBase(Guid session_id, string consumerKey, string consumerSecret)
        {
            try
            {
                this.SessionId      = session_id;
                this.ConsumerKey    = consumerKey;
                this.ConsumerSecret = consumerSecret;
                this.AccessToken    = this.GetSession<AccessToken>();
                this.IsAuthenticated = true;
                this.SetupClient(OAuthType.ProtectedResource);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Constructor when the login process has been done, the session_id will retrieve
        /// automatically the Access Token & Secret
        /// </summary>
        /// <param name="session_id"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        public OAuthBase(Guid sessionId, string consumerKey, string consumerSecret, string oauthVerifier, string accessTokenUrl)
        {
            try
            {
                this.SessionId = sessionId;
                this.ConsumerKey = consumerKey;
                this.ConsumerSecret = consumerSecret;
                this.AccessTokenUrl = accessTokenUrl;
                this.SetupClient(OAuthType.AccessToken);

                this.GetAccessToken(oauthVerifier);

                this.SetupClient(OAuthType.ProtectedResource);

                this.IsAuthenticated = true;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Constructor for manually setting the Access Key and Access Token.
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="accessKey"></param>
        /// <param name="accessSecret"></param>
        public OAuthBase(string consumerKey, string consumerSecret, string accessKey, string accessSecret)
        {
            this.SessionId = Guid.Empty;
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
            this.AccessToken = new AccessToken(accessKey, accessSecret);
            this.IsAuthenticated = true;
            
            this.SetupClient(OAuthType.ProtectedResource);

        }

        /// <summary>
        /// Sets the appropiate client
        /// </summary>
        /// <param name="type"></param>
        private void SetupClient(OAuthType type)
        {
            OAuthCredentials credentials = new OAuthCredentials()
            {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader
            };

            credentials.Type = type;
            
            if(AccessToken != null) {
                credentials.Token = AccessToken.Key;
                credentials.TokenSecret = AccessToken.Secret;
            }

            this.Client = new RestClient()
            {
                Credentials = credentials
            };
        }
        #endregion

        #region Token Helpers

        /// <summary>
        /// Gets the Request token, first part of the OAuth dance.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected RequestToken GetRequestToken(Parameters parameters) {
            parameters.Domain = ConsumerKey;

            RestRequest request = new RestRequest() {
                Path = RequestTokenUrl, 
                Method = WebMethod.Post, 
                Credentials = new OAuthCredentials() {
                    CallbackUrl = CallbackUrl, 
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    ParameterHandling = OAuthParameterHandling.UrlOrPostParameters,
                    Type = OAuthType.RequestToken,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                }
            }
            .AddParameters(parameters.ToDictionary());

            RestResponse response = Client.Request(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }

            string[] sp = response.Content.Split('&');
            RequestToken t = new RequestToken(sp[0].Split('=').Last(), sp[1].Split('=').Last());
            this.SetAuthorizationUrl(t, parameters);

            this.SaveSession(t);

            return t;
        }

        /// <summary>
        /// Gets the Access Token required for consuming services.
        /// </summary>
        /// <param name="session_id"></param>
        /// <param name="verifier"></param>
        public void GetAccessToken(string verifier)
        {
            RequestToken rt = this.GetSession<RequestToken>();

            RestRequest request = new RestRequest()
            {
                Path = AccessTokenUrl,
                Method = WebMethod.Get,
                Credentials = new OAuthCredentials()
                {
                    CallbackUrl = CallbackUrl,
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    Token = rt.Key,
                    TokenSecret = rt.Secret,
                    Verifier = verifier,
                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                    Type = OAuthType.AccessToken,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                }
            } ;
            
            RestResponse response = Client.Request(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new InvalidTokenException();
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }

            string[] sp = response.Content.Split('&');
            AccessToken at = new AccessToken(sp[0].Split('=').Last(), sp[1].Split('=').Last());
            this.SaveSession(at);
            this.AccessToken = at;

            Client = new RestClient()
            {
                Credentials = new OAuthCredentials()
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    Token = AccessToken.Key,
                    TokenSecret = AccessToken.Secret,
                    Type = OAuthType.ProtectedResource,
                    ParameterHandling = OAuthParameterHandling.UrlOrPostParameters
                }
            };
        }

        /// <summary>
        /// This returns the redirect url. 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private void SetAuthorizationUrl(RequestToken token, Parameters parameters)
        {
           token.AuthorizationUrl = string.Format(AuthorizationUrl + "?oauth_token={0}&oauth_callback={1}&domain={2}&location={3}&granularity={4}&scope={5}", 
                    token.Key, CallbackUrl, ConsumerKey, parameters.Location, parameters.Granularity, parameters.Scope);
           
        }
        
        #endregion

        #region Session

        private static Dictionary<string, Token> _tokens = new Dictionary<string,Token>();

        /// <summary>
        /// Saves to session storage the specified token
        /// </summary>
        /// <param name="type"></param>
        /// <param name="token"></param>
        protected void SaveSession(Token token)
        {
            string key = this.SessionId.ToString() + "-" + token.GetType().ToString();
            SessionStorage.Set(key, token);
        }

        public static bool ExistsAccessToken(Guid sessionId)
        {
            string key = sessionId.ToString() + "-" + typeof(AccessToken).ToString();
            return SessionStorage.Exists(key);
        }

        /// <summary>
        /// Gets from session storage the specified token
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected T GetSession<T>() where T:Token
        {
            string key = this.SessionId.ToString() + "-" + typeof(T).ToString();
            Token temp = SessionStorage.Exists(key) ? SessionStorage.Get(key) : null; //_tokens.ContainsKey(key) ? _tokens[key] : null;

            if (temp == null)
            {
                throw new TokenNotFoundException(key);
            }

            return (T)temp;
        }

        #endregion

        #region Authenticated Actions

        /// <summary>
        /// Performs an HTTP GET action over the specified resource.
        /// </summary>
        /// <param name="resource"></param>
        protected T Get<T>(string resource, Dictionary<string, string> parameters) {
            RestRequest request = new RestRequest()
            {
                Method = WebMethod.Get,
                Path = resource
            };

            if (parameters != null && parameters.Count > 0)
            {
                request.AddParameters(parameters);
            }

            RestResponse response = Client.Request(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }

            return Common7.Models.Loader.Parse<T>(response.Content);
        }
        
        /// <summary>
        /// Performs an HTTP POST action over the specified resource.
        /// </summary>
        /// <param name="resource"></param>
        protected T Post<E, T>(string resource, E entity, Dictionary<string, string> parameters) {
            RestRequest request = new RestRequest()
            {
                Method = WebMethod.Post,
                Path = resource,
                Credentials = Client.Credentials
            };

            if (parameters != null && parameters.Count > 0)
            {
                request.AddParameters(parameters);
            }

            request.AddPostContent(Encoding.Default.GetBytes(Loader.ToJson(entity)));
            Client.AddPostContent(Encoding.Default.GetBytes(Loader.ToJson(entity)));

            RestResponse response = Client.Request(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }

            return Common7.Models.Loader.Parse<T>(response.Content);
        }

        protected void Post<E>(string resource, E entity, Dictionary<string, string> parameters) {
            
            RestRequest request = new RestRequest()
            {
                Method = WebMethod.Post,
                Path = resource,
                Credentials = Client.Credentials,
                QueryHandling = Hammock.QueryHandling.None
            };
            
            Client.QueryHandling = Hammock.QueryHandling.None;

            request.AddHeader("Content-Type", "application/json");

            if (parameters != null && parameters.Count > 0)
            {
                request.AddParameters(parameters);
            }
            // "Content-Type: multipart/form-data" 
            string str = Loader.ToJson(entity);
            request.AddPostContent(Encoding.UTF8.GetBytes(str));
            Client.AddPostContent(Encoding.UTF8.GetBytes(str));
            
            RestResponse response = Client.Request(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new GenericErrorException(Loader.Parse<ErrorContainer>(response.Content));
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new Exception(response.StatusDescription);
            }

        }


        /// <summary>
        /// Performs an HTTP DELETE action over the specified resource.
        /// </summary>
        /// <param name="resource"></param>
        protected T Delete<T>(string resource, Dictionary<string, string> parameters)
        {
            RestRequest request = new RestRequest()
            {
                Method = WebMethod.Delete,
                Path = resource,
                Credentials = Client.Credentials,
            };


            if (parameters != null && parameters.Count > 0)
            {
                request.AddParameters(parameters);
            }

            RestResponse response = Client.Request(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }

            return Common7.Models.Loader.Parse<T>(response.Content);
        }

        /// <summary>
        /// Performs an HTTP DELETE action over the specified resource.
        /// </summary>
        /// <param name="resource"></param>
        protected void Delete(string resource, Dictionary<string, string> parameters)
        {
            RestRequest request = new RestRequest()
            {
                Method = WebMethod.Delete,
                Path = resource,
                Credentials = Client.Credentials,
            };


            if (parameters != null && parameters.Count > 0)
            {
                request.AddParameters(parameters);
            }

            RestResponse response = Client.Request(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new Exception(response.StatusDescription);
            }

        }

        #endregion
    }
}
