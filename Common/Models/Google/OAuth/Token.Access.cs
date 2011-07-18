using System;
using System.Runtime.Serialization;

namespace Common7.Models.Google.OAuth
{
    [DataContract]
    public class AccessToken : Token
    {
        [DataMember(Name = "created_on")]
        public DateTime CreatedOn;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        public AccessToken(string key, string secret) : base(key, secret) { }
    }
}
