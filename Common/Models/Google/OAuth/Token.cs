using System;
using System.Runtime.Serialization;

namespace Common7.Models.Google.OAuth
{
    /// <summary>
    /// Base Token class. Contains a simple pair of key/secret. 
    /// </summary>
    [DataContract]
    public class Token
    {
        /// <summary>
        /// Key
        /// </summary>
        
        [DataMember(Name = "key")]
        public string Key { get; set; }
        
        /// <summary>
        /// Secret
        /// </summary>
        [DataMember(Name = "secret")]
        public string Secret { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        public Token(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }

    }
}
