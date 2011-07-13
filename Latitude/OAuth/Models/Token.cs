using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Latitude.OAuth.Models
{
    class Token
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public Token(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }
    }
}
