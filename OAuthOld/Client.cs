using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAuth
{
    public class Client
    {
        public string ConsumerKey {get; private set;}
        public string ConsumerSecret {get; private set;}
        public string RequestTokenUrl {get; private set;}
        public string AccessTokenUrl {get; private set;}
        public string AuthorizationUrl {get; private set;}

        public Client(string consumer_key, string consumer_token, string request_token_url,
                        string access_token_url, string authorization_url) {
            ConsumerKey = consumer_key;
            ConsumerSecret = consumer_token;
            RequestTokenUrl = request_token_url;
            AccessTokenUrl = access_token_url;
            AuthorizationUrl = authorization_url;
        }
    
        public void FetchRequestToken() {
            /* def fetch_request_token(self, oauth_request):
        """-> OAuthToken."""
        # Using headers or payload varies by service...
        response = urlfetch.fetch(
            url=self.request_token_url,
            method=oauth_request.http_method,
            #headers=oauth_request.to_header(),
            payload=oauth_request.to_postdata())
        return oauth.OAuthToken.from_string(response.content)/*/
        }
   
        public void FetchAccessToken() {
        /*def fetch_access_token(self, oauth_request):
        """-> OAuthToken."""
        response = urlfetch.fetch(
            url=self.access_token_url,
            method=oauth_request.http_method,
            headers=oauth_request.to_header())
        return oauth.OAuthToken.from_string(response.content)*/
        }

        public void AccessResource() {
            /*
    def access_resource(self, oauth_request, deadline=None):
        """-> Some protected resource."""
        if oauth_request.http_method == 'GET':
            url = oauth_request.to_url()
            return urlfetch.fetch(
                url=url,
                method=oauth_request.http_method)
        else:
            payload = oauth_request.to_postdata()
            return urlfetch.fetch(
                url=oauth_request.get_normalized_http_url(),
                method=oauth_request.http_method,
                payload=payload)
             * */
        }
    }
    
}
