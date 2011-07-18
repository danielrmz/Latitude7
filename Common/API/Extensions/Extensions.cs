using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammock;
using Hammock.Web;
using Hammock.Authentication.OAuth;
using System.Reflection;

namespace Latitude7.API
{
    internal static class Extensions
    {
        internal static RestRequest AddParameters(this RestRequest self, Dictionary<string, string> parameters) {
            List<KeyValuePair<string, string>> list = parameters.ToList();
            if (list.Count > 0)
            {
                list.ForEach(kvp => self.AddParameter(kvp.Key, kvp.Value));
            }

            return self;
        }

        internal static string FormatWith(this string self, params object[] argset)
        {
            return String.Format(self, argset);
        }

    }
}
