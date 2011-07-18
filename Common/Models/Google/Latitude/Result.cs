using System;
using System.Runtime.Serialization;

namespace Common7.Models.Google.Latitude
{
    [DataContract]
    public class Result<T>
    {
        [DataMember(Name = "data")]
        public T data { get; set; }
    }
}
