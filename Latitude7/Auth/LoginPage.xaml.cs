using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;
using Models.Google.OAuth;
using Models;

namespace Latitude7.Auth
{
    public partial class LoginPage : UserControl
    {
        public const string CONSUMER_KEY = "522970715419.apps.googleusercontent.com";
        public const string CONSUMER_SECRET = "yJexLLuB6Z2bA1tEVB23Fa6T";


        public LoginPage()
        {
            InitializeComponent();
            
        }

        private void cmdLogin_Click(object sender, RoutedEventArgs e)
        {
            /*var credentials = new OAuthCredentials
            {
                Type = OAuthType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = CONSUMER_KEY,
                ConsumerSecret = CONSUMER_SECRET,
                CallbackUrl = "urn:ietf:wg:oauth:2.0:oob",
            };

            RestClient client = new RestClient()
            {
                //Authority = "https://www.googleapis.com/auth/latitude",
                //Credentials = credentials
            };

            RestRequest req = new RestRequest()
            {
                //  Path = "https://www.google.com/accounts/OAuthGetRequestToken",
                Path = "https://accounts.google.com/o/oauth2/device/code",

            };

            req.AddField("client_id", CONSUMER_KEY);
            req.AddField("scope", "https://www.googleapis.com/auth/latitude");
            req.AddField("redirect_uri",  "urn:ietf:wg:oauth:2.0:oob");
            req.AddField("response_type", "code");

            client.BeginRequest(req, new RestCallback(LoginCallback));   */
            string auth_uri = "https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&scope={2}&response_type=code";
            browser.Source = new Uri(string.Format(auth_uri, CONSUMER_KEY, "urn:ietf:wg:oauth:2.0:oob", "https://www.googleapis.com/auth/latitude"));

        }



        private void LoginCallback(RestRequest request, RestResponse response, object userState)
        {
            VerificationToken token = Loader.Parse<VerificationToken>(response.Content);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string auth_uri = "https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&scope={2}&response_type=code";
            browser.Source = new Uri(string.Format(auth_uri, CONSUMER_KEY, "urn:ietf:wg:oauth:2.0:oob", "https://www.googleapis.com/auth/latitude"));
        }

        private void browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Uri uri = e.Uri;
        }

        private void browser_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void browser_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void browser_Navigating(object sender, Microsoft.Phone.Controls.NavigatingEventArgs e)
        {

        }
       
    }
}
