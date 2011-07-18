using System;
using Common7.Models.Google.Common;

namespace Latitude7.API.Exceptions
{
    public class GenericErrorException : Exception
    {
        public ErrorContainer Object { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="error"></param>
        public GenericErrorException(ErrorContainer error)
        {
            Object = error;
        }

    }
}
