using System;
using System.Net;
using System.Collections.Generic;

namespace Common7.Models.Google.Latitude
{
    public class BaseParameters
    {

        public string Scope { get; set; }
        public string Domain { get; set; }
        public string Granularity { get; set; }
        public string Location { get; set; }



        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Scope))
            {
                parameters.Add("scope", Scope);
            }

            if (!string.IsNullOrEmpty(Domain))
            {
                parameters.Add("domain", Domain);
            }

            if (!string.IsNullOrEmpty(Granularity))
            {
                parameters.Add("granularity", Granularity);
            }

            if (!string.IsNullOrEmpty(Location))
            {
                parameters.Add("location", Location);
            }

            return parameters;
        }
    }
}
