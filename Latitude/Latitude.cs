using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;

using Latitude.OAuth.Models;
using Latitude.OAuth;

using System.Web;
using System.Web.Mvc;

namespace Latitude
{
    public class Latitude
    {
        public const string REQUEST_TOKEN_URL = "https://www.google.com/accounts/OAuthGetRequestToken";
        public const string ACCESS_TOKEN_URL  = "https://www.google.com/accounts/OAuthGetAccessToken";
        public const string AUTHORIZATION_URL = "https://www.google.com/latitude/apps/OAuthAuthorizeToken";
        public const string SCOPE             = "https://www.googleapis.com/auth/latitude";
        public const string REST_URL          = "https://www.googleapis.com/latitude/v1/%s";

        private Client Client { get; set; }
        public bool IsAuthenticated { get; set; }

        public Latitude(string consumerKey, string consumerSecret, string callbackUrl)
        {
            IsAuthenticated = false;
            Client = new Client(consumerKey, consumerSecret, callbackUrl, REQUEST_TOKEN_URL, ACCESS_TOKEN_URL, AUTHORIZATION_URL);
        }

        public Latitude(string consumerKey, string consumerSecret, string accessKey, string accessSecret)
        {
            IsAuthenticated = true;
            Client = new Client(consumerKey, consumerSecret, accessKey, accessSecret, REQUEST_TOKEN_URL, ACCESS_TOKEN_URL, AUTHORIZATION_URL);
        }

        public void RedirectToAuthorizationPage(Controller controller)
        {
            string url = Client.GetAuthorizationRedirectUrl(new ConfigParameters()
            {
                Domain = "localhost",
                Granularity = "best",
                Location = "all",
                Scope = SCOPE
            });

            controller.Response.Redirect(url, true);
            
        }

        public void GetCurrentLocation()
        {
        }

        public void UpdateCurrentLocation()
        {
        }

        public void DeleteCurrentLocation()
        {
        }

        public void ListLocations()
        {
        }

        public void GetLocation()
        {
        }

        public void UpdateLocation()
        {
        }

        public void DeleteLocation()
        {
        }

    }
}
