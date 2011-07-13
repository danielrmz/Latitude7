using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAuth
{
    /// <summary>
    /// Latitude-specific OAuth client
    /// </summary>
    class LatitudeClient : Client
    {
        // http://code.google.com/apis/gdata/articles/oauth.html

        protected const string REQUEST_TOKEN_URL = "https://www.google.com/accounts/OAuthGetRequestToken";
        protected const string ACCESS_TOKEN_URL  = "https://www.google.com/accounts/OAuthGetAccessToken";
        protected const string AUTHORIZATION_URL = "https://www.google.com/latitude/apps/OAuthAuthorizeToken";
        protected const string SCOPE             = "https://www.googleapis.com/auth/latitude";
        protected const string REST_URL          = "https://www.googleapis.com/latitude/v1/%s";

        public LatitudeClient(string oauth_key, string oauth_secret) 
            : base(oauth_key, oauth_secret, REQUEST_TOKEN_URL, ACCESS_TOKEN_URL, AUTHORIZATION_URL) {
            //self.signature_method = oauth.OAuthSignatureMethod_HMAC_SHA1()
        }

        public void GetCurrentLocation() {
            /* request = oauth.OAuthRequest.from_consumer_and_token(
            self.client.get_consumer(),
            token=self.client.get_token(),
            http_method='GET',
            http_url=Latitude.REST_URL % ('currentLocation',),
            parameters={'granularity': 'best'})

        request.sign_request(
            self.client.signature_method,
            self.client.get_consumer(),
            self.client.get_token())

        return self.client.access_resource(request)*/

        }
    }
}

