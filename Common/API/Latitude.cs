using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;

using Common7.Models.Google.OAuth;
using Common7.Models.Google.Latitude;
using Common7.Models.Google.Common;

namespace Latitude7.API
{
    public partial class Latitude : API.Base.OAuthBase
    {
        #region Constants related to Google Latitude API

        private const string REQUEST_TOKEN_URL = "https://www.google.com/accounts/OAuthGetRequestToken";
        private const string ACCESS_TOKEN_URL  = "https://www.google.com/accounts/OAuthGetAccessToken";
        private const string AUTHORIZATION_URL = "https://www.google.com/latitude/apps/OAuthAuthorizeToken";
        private const string SCOPE             = "https://www.googleapis.com/auth/latitude";
        private const string REST_URL          = "https://www.googleapis.com/latitude/v1/{0}?key={1}";
        private const string REST_URL_NOSSL    = "http://www.googleapis.com/latitude/v1/{0}?key={1}";

        /// <summary>
        /// API Key provided by Google. (Google-specific parameter for accessing resources)
        /// </summary>
        private string API_KEY = "";

        private Parameters BASE_PARAMETERS = new Parameters()
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
        /// Gets the Current Location Resource.
        /// 
        /// currentLocation.get	GET  /currentLocation	AUTHENTICATED
        /// </summary>
        public Location GetCurrentLocation()
        {
            Result<Location> location = Get<Result<Location>>(REST_URL.FormatWith("currentLocation", API_KEY), BASE_PARAMETERS.ToDictionary());
            return location.data;
        }

        /// <summary>
        /// Inserts and updates the current location. Additionally it adds an entry to the Location History Collection
        /// currentLocation.insert	POST  /currentLocation	AUTHENTICATED
        /// </summary>
        public Location InsertCurrentLocation(Location location)
        {
            Post<Result<Location>>(REST_URL.FormatWith("currentLocation", API_KEY), new Result<Location>() { data = location }, null);
            return location;
        }

        /// <summary>
        /// Deletes/Hides the current location.
        /// 
        /// currentLocation.delete	DELETE  /currentLocation	AUTHENTICATED
        /// </summary>
        public Location DeleteCurrentLocation()
        {
            Location currentLocation = this.GetCurrentLocation();
            Delete(REST_URL.FormatWith("currentLocation", API_KEY), null);

            return currentLocation;
        }

        /// <summary>
        /// Gets the History of the locations. 
        /// 
        /// Applicable parameters (using the Latitude.Parameters class):
        ///     granularity  The granularity of the locations to return. Can be either city or best. The default is city.
        ///     min-time   The minimum timestamp of the locations to return (in milliseconds since the epoch).
        ///     max-time   The maximum timestamp of the locations to return (in milliseconds since the epoch).
        ///     max-results   The maximum number of locations to return. The default is 100, and the maximum is 1000.
        ///
        /// location.list	GET  /location	AUTHENTICATED
        /// </summary>
        public List<Location> ListLocations(Parameters parameters)
        {
            Result<History> location = Get<Result<History>>(REST_URL.FormatWith("location", API_KEY), null);
            return location.data.items;
        }

        /// <summary>
        /// Gets the specified location
        /// 
        /// location.get	GET  /location/locationId	AUTHENTICATED
        /// </summary>
        /// <param name="locationId">Location id</param>
        /// <returns>Location</returns>
        public Location GetLocation(string locationId)
        {
            Result<Location> location = Get<Result<Location>>(REST_URL.FormatWith("location/{0}", API_KEY).FormatWith(locationId), null);
            return location.data;
        }

        /// <summary>
        /// Creates a new location on the specified timestamp which is the primary key
        /// 
        /// location.insert	POST  /location	AUTHENTICATED
        /// </summary>
        /// <param name="location">The location to be inserted.</param>
        /// <returns>Location</returns>
        public Location InsertLocation(Location location)
        {
            Post<Result<Location>>(REST_URL.FormatWith("location", API_KEY), new Result<Location>() { data = location }, null);
            return location;
        }

        /// <summary>
        /// Deletes a Location entry from the Location History collection
        /// 
        /// location.delete	DELETE  /location/locationId	AUTHENTICATED
        /// </summary>
        /// <param name="locationId">The id/timestamp of the entry to be deleted.</param>
        /// <returns>Location</returns>
        public Location DeleteLocation(string locationId)
        {
            Location location = this.GetLocation(locationId);
            Delete(REST_URL.FormatWith("location/{0}", API_KEY).FormatWith(locationId), null);

            return location;
        }

        #endregion
    }
}
