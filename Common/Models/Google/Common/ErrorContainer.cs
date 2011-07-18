using System;
using System.Runtime.Serialization;

namespace Common7.Models.Google.Common
{
    [DataContract]
    public class ErrorContainer
    {
         [DataMember(Name = "error")]
        public Error error { get; set; }
    }
}
