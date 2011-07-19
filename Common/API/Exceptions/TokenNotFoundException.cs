using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Latitude7.API.Exceptions
{
    class TokenNotFoundException : Exception
    {
        public TokenNotFoundException(string tokenId) : base("Token with id: " + tokenId + " was not found") { }
    }
}
