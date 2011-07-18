using System;
using System.Runtime.Serialization;

namespace Common7.Models.Google.Latitude
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "kind")]
        public string kind { get;set;}

        [DataMember(Name = "timestampMs")]
        public double timeStamp { get; set ;}

        [DataMember(Name = "latitude")]
        public double latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double longitude { get; set; }

        [DataMember(Name = "accuracy")]
        public double accuracy { get; set; }

        [DataMember(Name = "speed")]
        public double speed { get; set; }

        [DataMember(Name = "heading")]
        public double heading { get; set; }

        [DataMember(Name = "altitude")]
        public double altitude { get; set; }

        [DataMember(Name = "altitudeAccuracy")]
        public double altitudeAccuracy { get; set;}

    }
}
