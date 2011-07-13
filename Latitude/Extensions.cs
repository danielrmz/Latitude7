using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammock;
using Hammock.Web;
using Hammock.Authentication.OAuth;

namespace Latitude
{
    static class Extensions
    {
        public static RestRequest AddParameters(this RestRequest self, Dictionary<string, string> parameters) {
            List<KeyValuePair<string, string>> list = parameters.ToList();
            if (list.Count > 0)
            {
                list.ForEach(kvp => self.AddParameter(kvp.Key, kvp.Value));
            }

            return self;
        }

    }
}
