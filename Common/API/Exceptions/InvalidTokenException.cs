using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Latitude7.API.Exceptions
{
    class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base("The specified token was invalid.") { }
    }
}
