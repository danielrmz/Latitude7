using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Common7.Models.Google.OAuth
{
    /// <summary>
    /// Request Token. First part of the OAuth process.
    /// </summary>
    [DataContract]
    public class RequestToken : Token
    {
        /// <summary>
        /// Represents the complete authorization url. The user has to be redirected
        /// to this page to authorize the request tokens.
        /// </summary>
        [DataMember(Name = "authorization_url")]
        public string AuthorizationUrl { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        public RequestToken(string key, string secret) : base(key, secret) { }

    }
}
