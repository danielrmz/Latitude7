using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Common7.Models.Google.Latitude
{
    [DataContract]
    public class History
    {
        [DataMember(Name = "items")]
        public List<Location> items { get; set; }
    }
}
