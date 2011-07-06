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
using Microsoft.Phone.Controls;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;
using System.Text.RegularExpressions;

namespace LatitudeMarker
{
    public partial class MainPage : PhoneApplicationPage
    {
        public const string CONSUMER_KEY = "522970715419.apps.googleusercontent.com";
        public const string CONSUMER_SECRET = "yJexLLuB6Z2bA1tEVB23Fa6T";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var credentials = new OAuthCredentials
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
                Authority = "https://www.googleapis.com/auth/latitude",
                Credentials = credentials
            };
        
            RestRequest req = new RestRequest()
            {
                Path = "https://www.google.com/accounts/OAuthGetRequestToken",
               
            };
            
            client.BeginRequest(req, new RestCallback(LoginCallback));        

        }

        private void LoginCallback(RestRequest request, RestResponse response, object userState)
        {
            Regex r = new Regex("&lt;oauth_token&gt;(.*?)&lt;/oauth_token&gt;&lt;oauth_verifier&gt;(.*?)&lt;/oauth_verifier&gt;");
            var match = r.Match(response.Content);
            txtResults.Text = response.Content;
        }
    }
}