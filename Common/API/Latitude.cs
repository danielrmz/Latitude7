using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;

using Common7.Models.Google.OAuth;
using Common7.Models.Google.Latitude;

namespace Latitude7.API
{
    public partial class Latitude : API.Base.OAuthBase
    {
        #region Constants related to Google Latitude API

        private const string REQUEST_TOKEN_URL = "https://www.google.com/accounts/OAuthGetRequestToken";
        private const string ACCESS_TOKEN_URL = "https://www.google.com/accounts/OAuthGetAccessToken";
        private const string AUTHORIZATION_URL = "https://www.google.com/latitude/apps/OAuthAuthorizeToken";
        private const string SCOPE = "https://www.googleapis.com/auth/latitude";
        private const string REST_URL = "https://www.googleapis.com/latitude/v1/{0}?key={1}";
        private const string REST_URL_NOSSL = "http://www.googleapis.com/latitude/v1/{0}?key={1}";

        /// <summary>
        /// API Key provided by Google. (Google-specific parameter for accessing resources)
        /// </summary>
        private string API_KEY = "";

        private BaseParameters BASE_PARAMETERS = new BaseParameters()
        {
                Granularity = "best",
                Location = "all",
                Scope = SCOPE
        };

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="callbackUrl"></param>
        public Latitude(Guid sessionId, string apiKey, string consumerKey, string consumerSecret, Uri callbackUrl)
            : base(sessionId, consumerKey, consumerSecret, callbackUrl, REQUEST_TOKEN_URL, ACCESS_TOKEN_URL, AUTHORIZATION_URL)
        {
            this.BASE_PARAMETERS.Domain = consumerKey;
            this.API_KEY = apiKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="accessKey"></param>
        /// <param name="accessSecret"></param>
        public Latitude(Guid sessionId, string apiKey, string consumerKey, string consumerSecret, string accessKey, string accessSecret)
            : base(sessionId, consumerKey, consumerSecret, accessKey, accessSecret, REQUEST_TOKEN_URL, ACCESS_TOKEN_URL, AUTHORIZATION_URL)
        {
            this.BASE_PARAMETERS.Domain = consumerKey;
            this.API_KEY = apiKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        public Latitude(Guid sessionId, string apiKey, string consumerKey, string consumerSecret)
            : base(sessionId, consumerKey, consumerSecret)
        {
            this.BASE_PARAMETERS.Domain = consumerKey;
            this.API_KEY = apiKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="oauthVerifier"></param>
        public Latitude(Guid sessionId, string apiKey, string consumerKey, string consumerSecret, string oauthVerifier)
            : base(sessionId, consumerKey, consumerSecret, oauthVerifier, ACCESS_TOKEN_URL)
        {
            this.BASE_PARAMETERS.Domain = consumerKey;
            this.API_KEY = apiKey;
        }

        #endregion

        #region Main API

        /// <summary>
        /// currentLocation.get	GET  /currentLocation	AUTHENTICATED
        /// </summary>
        public Location GetCurrentLocation()
        {
            Result<Location> location = Get<Result<Location>>(REST_URL.FormatWith("currentLocation", API_KEY), BASE_PARAMETERS.ToDictionary());
            return location.data;
        }

        /// <summary>
        /// currentLocation.insert	POST  /currentLocation	AUTHENTICATED
        /// </summary>
        public Location InsertCurrentLocation(Location location)
        {
            Post<Location>(REST_URL.FormatWith("currentLocation", API_KEY), location, null);
            return location;
        }

        /// <summary>
        /// currentLocation.delete	DELETE  /currentLocation	AUTHENTICATED
        /// </summary>
        public Location DeleteCurrentLocation()
        {
            Location currentLocation = this.GetCurrentLocation();
            Delete(REST_URL.FormatWith("currentLocation", API_KEY), null);

            return currentLocation;
        }

        /// <summary>
        /// location.list	GET  /location	AUTHENTICATED
        /// </summary>
        public void ListLocations()
        {
        }

        /// <summary>
        /// location.get	GET  /location/locationId	AUTHENTICATED
        /// </summary>
        public void GetLocation()
        {
        }

        /// <summary>
        /// location.insert	POST  /location	AUTHENTICATED
        /// </summary>
        public void InsertLocation()
        {
        }

        /// <summary>
        /// location.delete	DELETE  /location/locationId	AUTHENTICATED
        /// </summary>
        public void DeleteLocation()
        {
        }

        #endregion
    }
}
