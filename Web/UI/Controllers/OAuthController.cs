using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using Common7.Models.Google.Latitude;

using Latitude7.API.Exceptions;

namespace Latitude7.UI.Controllers
{
    using Latitude = Latitude7.API.Latitude;
    using System.Text.RegularExpressions;

    public class OAuthController : Controller
    {
        //
        // GET: /OAuth/

        public ActionResult Index()
        {
            Guid session_id = new Guid("80bafe28-3a4e-4645-9afd-0389b4e3ec38");// Guid.NewGuid();
            
            Latitude api = this.GetAPI(session_id);

            if (api.IsAuthenticated)
            {
                try
                {
                    return Json(api.InsertCurrentLocation(new Location()
                                {
                                    latitude = 25.599575,
                                    longitude = -100.265839, 
                                    kind = "latitude#location",
                                    accuracy = 0,
                                    timeStamp = timestamp() * 1000
                                }), JsonRequestBehavior.AllowGet);
                }
                catch (GenericErrorException gee)
                {
                    return this.ExceptionToJson(gee);
                }
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /OAuth/Callback

        public ActionResult Callback(string oauth_verifier, string oauth_token)
        {
            Guid session_id = new Guid(Regex.Match(this.Request.RawUrl, "oauth/callback/(.*)").Groups[1].Value.Split('?').First());

            try
            {
                Latitude api = this.GetAPI(session_id, oauth_verifier);
            
                return Json(api.InsertCurrentLocation(new Location()
                            {
                                latitude = 25.599575,
                                longitude = -100.265839,
                                kind = "latitude#location",
                                accuracy = 0,
                                timeStamp = timestamp() * 1000
                            }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return this.ExceptionToJson(e);
            }
        }

        public Latitude GetAPI(Guid sessionId)
        {
            return this.GetAPI(sessionId, string.Empty);
        }

        public Latitude GetAPI(Guid sessionId, string oauthVerifier)
        {
            Latitude api;
            if (Latitude.ExistsAccessToken(sessionId))
            {
                api = new Latitude(sessionId,
                                   ConfigurationManager.AppSettings["Google.Latitude.APIKey"],
                                   ConfigurationManager.AppSettings["Google.Latitude.OAuth.Key"],
                                   ConfigurationManager.AppSettings["Google.Latitude.OAuth.Secret"]);
            }
            else
            {
                if (!string.IsNullOrEmpty(oauthVerifier))
                {
                    api = new Latitude(sessionId,
                                        ConfigurationManager.AppSettings["Google.Latitude.APIKey"],
                                        ConfigurationManager.AppSettings["Google.Latitude.OAuth.Key"],
                                        ConfigurationManager.AppSettings["Google.Latitude.OAuth.Secret"],
                                        oauthVerifier);
                }
                else
                {
                    api = new Latitude(sessionId,
                                                ConfigurationManager.AppSettings["Google.Latitude.APIKey"],
                                                ConfigurationManager.AppSettings["Google.Latitude.OAuth.Key"],
                                                ConfigurationManager.AppSettings["Google.Latitude.OAuth.Secret"],
                                                new Uri(string.Format("http://localhost:8080/oauth/callback/{0}", sessionId.ToString()))
                                                );

                    api.RedirectToAuthorizationPage(this);
                }
            }

            return api;
        }

        public JsonResult ExceptionToJson(Exception e)
        {
            if (e.GetType().Equals(typeof(GenericErrorException)))
            {
                return Json(((GenericErrorException)e).Object, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Message = e.Message,
                InnerException = e.InnerException,
                Source = e.Source,
                StackTrace = e.StackTrace
            }, JsonRequestBehavior.AllowGet);
        }

        public double timestamp() {
            TimeSpan unix_time = (System.DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return unix_time.TotalSeconds;
        }

    }
}
