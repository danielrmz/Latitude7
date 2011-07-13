using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using Latitude;

namespace WebUI.Controllers
{
    public class OAuthController : Controller
    {

        public ActionResult Index()
        {
            string consumer_key    = ConfigurationManager.AppSettings["OAuth.Google.Key"];
            string consumer_secret = ConfigurationManager.AppSettings["OAuth.Google.Secret"];
 
            Latitude.Latitude client = new Latitude.Latitude(consumer_key, consumer_secret, "http://localhost:8080/oauth/callback");
            client.RedirectToAuthorizationPage(this);

            return View();
        }

        public ActionResult Callback()
        {
            return View();
        }

        
    }
}
