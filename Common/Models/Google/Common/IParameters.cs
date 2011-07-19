using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common7.Models.Google.Common
{
    public interface IParameters
    {
        Dictionary<string, string> ToDictionary();
    }
}
