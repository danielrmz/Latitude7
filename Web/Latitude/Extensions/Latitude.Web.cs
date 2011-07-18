using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Common7.Models.Google.Latitude;
using Common7.Models.Google.OAuth;

namespace Latitude7.API
{
    /// <summary>
    /// Specific MVC Partial class extension.
    /// </summary>
    public partial class Latitude
    {
        public void RedirectToAuthorizationPage(Controller controller)
        {
            RequestToken token = this.GetRequestToken(BASE_PARAMETERS);

            controller.Response.Redirect(token.AuthorizationUrl, true);
        
        }

    }
}
