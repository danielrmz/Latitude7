using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Latitude.OAuth.Models;
using Hammock;
using Hammock.Web;
using Hammock.Authentication.OAuth;

namespace Latitude.OAuth
{
    class Client 
    {
        private string ConsumerKey { get; set; }
        private string ConsumerSecret { get; set; }
        private string RequestKey { get; set; }
        private string RequestSecret { get; set; }

        private string RequestTokenUrl { get; set; }
        private string AccessTokenUrl { get; set; }
        private string AuthorizationUrl { get; set; }
        private string CallbackUrl { get; set; }

        private Hammock.RestClient _Client { get; set;}

        public Client(string consumerKey, string consumerSecret, string callbackUrl, string requestTokenUrl, 
                        string accessTokenUrl, string authorizationUrl)
        { 
            ConsumerKey      = consumerKey;
            ConsumerSecret   = consumerSecret;
            RequestTokenUrl  = requestTokenUrl;
            AccessTokenUrl   = accessTokenUrl;
            AuthorizationUrl = authorizationUrl;
            CallbackUrl      = callbackUrl;

            _Client = new RestClient() { };
        }

        public Client(string consumerKey, string consumerSecret, string accessKey, string accessSecret, string requestTokenUrl,
                        string accessTokenUrl, string authorizationUrl)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            RequestTokenUrl = requestTokenUrl;
            AccessTokenUrl = accessTokenUrl;
            AuthorizationUrl = authorizationUrl;
            RequestKey = accessKey;
            RequestSecret = accessSecret;

            _Client = new RestClient() {  
                Credentials = new OAuthCredentials() {  
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    Token = RequestKey,
                    TokenSecret = RequestSecret,
                    Type = OAuthType.ProtectedResource,
                    ParameterHandling = OAuthParameterHandling.UrlOrPostParameters
                }
            };
        }

        private Token GetRequestToken(ConfigParameters parameters) {
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

            RestResponse response = _Client.Request(request);

            string[] sp = response.Content.Split('&');
            Token t = new Token(sp[0].Split('=').Last(), sp[1].Split('=').Last());

            return t;
        }

        /// <summary>
        /// This returns the redirect url. 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetAuthorizationRedirectUrl(ConfigParameters parameters)
        {
            Token request_token = this.GetRequestToken(parameters);

            // Set to session RequestToken.Key, RequestToken.Secret
            
            // Redirect
            //domain={oauth_consumer_key}&oauth_callback={oauth_callback}&oauth_token={oauth_req uest_token_key}&scope={scope}&xoauth_displayname={display_name} 
            
            return string.Format(AuthorizationUrl + "?oauth_token={0}&oauth_callback={1}&domain={2}&location={3}&granularity={4}&scope={5}", 
                    request_token.Key, CallbackUrl, ConsumerKey, parameters.Location, parameters.Granularity, parameters.Scope);
        }
        
    }
}
