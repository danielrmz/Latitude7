using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAuth
{
    public class Helper
    {
        
        public Client OAuthClient { get; private set; }

        public Helper(Client client) {
            this.OAuthClient = client;
        }
        
        public string GetRequestToken(string callback, Dictionary<string, string> parameters) {
           /* GetRequestToken(self, callback, parameters=None):
        """Gets a request token from an OAuth provider."""
        request_token_request = oauth.OAuthRequest.from_consumer_and_token(
            self.oauth_client.get_consumer(),
            token=None,
            callback=callback,
            http_method='POST',
            http_url=self.oauth_client.request_token_url,
            parameters=parameters)

        # Request a token that we can use to redirect the user to an auth url.
        request_token_request.sign_request(
            self.oauth_client.signature_method,
            self.oauth_client.get_consumer(),
            None)
        return self.oauth_client.fetch_request_token(request_token_request)*/
            return string.Empty;
        }

        public void GetAuthorizationRedirectUrl(string request_token, Dictionary<string, string> parameters) {
        /*"""Gets the redirection URL for the OAuth authorization page."""
        authorization_request = oauth.OAuthRequest.from_token_and_callback(
            request_token,
            http_method='GET',
            http_url=self.oauth_client.authorization_url,
            parameters=parameters)
        return authorization_request.to_url()*/
        }

        public Token GetAccessToken(string request_token, string verifier) {
            /*"""Upgrades a request token to an access token."""
        access_request = oauth.OAuthRequest.from_consumer_and_token(
            self.oauth_client.get_consumer(),
            token=request_token,
            verifier=verifier,
            http_url=self.oauth_client.access_token_url)

        access_request.sign_request(
            self.oauth_client.signature_method,
            self.oauth_client.get_consumer(),
            request_token)
        return self.oauth_client.fetch_access_token(access_request)*/
            return new Token();
        }
    }
}
