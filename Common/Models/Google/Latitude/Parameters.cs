using System;
using System.Net;
using System.Collections.Generic;
using Common7.Models.Google.Common;

namespace Common7.Models.Google.Latitude
{
    /// <summary>
    /// Parameters used through the Latitude API.
    /// TODO: Separate in subclasses, those used for auth and for query handling.
    /// </summary>
    public class Parameters : IParameters
    {
        #region Authorization Params

        /// <summary>
        /// https://www.googleapis.com/auth/latitude
        /// Fixed/Static
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        ///  The domain used to identify your application.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// The range of locations you want to access. Can be either current or all. If this parameter is omitted, current is assumed.
        /// </summary>
        public string Location { get; set; }

        #endregion

        #region Collection-REST Params
        /// <summary>
        ///  The finest granularity of locations you want to access. Can be either city or best. If this parameter is omitted, city is assumed.
        /// </summary>
        public string Granularity { get; set; }

        /// <summary>
        /// The minimum timestamp of the locations to return (in milliseconds since the epoch).
        /// </summary>
        public string MinTime { get;set;}

        /// <summary>
        /// The maximum timestamp of the locations to return (in milliseconds since the epoch).
        /// </summary>
        public string MaxTime { get;set;}

        /// <summary>
        /// The maximum number of locations to return. The default is 100, and the maximum is 1000.
        /// </summary>
        public string MaxResults { get;set;}

        #endregion

        /// <summary>
        /// Simple method to extract them to a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            #region Base Parameters
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
            #endregion  
          
            #region List params

            if (!string.IsNullOrEmpty(MinTime)) {
                parameters.Add("min-time", MinTime);
            }

            if(!string.IsNullOrEmpty(MaxTime)) {
                parameters.Add("max-time", MaxTime);
            }
            if(!string.IsNullOrEmpty(MaxResults)) {
                parameters.Add("max-results", MaxResults);
            }
            
            #endregion

            return parameters;
        }
    }
}
