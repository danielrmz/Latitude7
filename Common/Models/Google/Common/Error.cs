using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Common7.Models.Google.Common
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "code")]
        public int code { get; set; }
         
        [DataMember(Name = "message")]
        public string message { get; set; }

        [DataMember(Name = "errors")]
        public List<SpecificError> errors { get; set; }
    }

}
