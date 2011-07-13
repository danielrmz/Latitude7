using System;
using System.Runtime.Serialization;
namespace Models.Google.OAuth
{
    [DataContract]
    public class VerificationToken
    {
        [DataMember(Name = "device_code")]
        public string DeviceCode;

        [DataMember(Name = "user_code")]
        public string UserCode;
        
        [DataMember(Name = "verification_url")]
        public string VerificationUrl;

        [DataMember(Name = "expires_in")]
        public int ExpiresIn;

        [DataMember(Name = "interval")]
        public int Interval;

    }
}
