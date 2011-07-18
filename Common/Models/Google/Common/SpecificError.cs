using System;
using System.Runtime.Serialization;

namespace Common7.Models.Google.Common
{
    [DataContract]
    public class SpecificError
    {
        [DataMember(Name = "domain")]
        public string domain { get; set; }

        [DataMember(Name = "reason")]
        public string reason { get; set; }

        [DataMember(Name = "message")]
        public string message { get; set; }  
    }
}
