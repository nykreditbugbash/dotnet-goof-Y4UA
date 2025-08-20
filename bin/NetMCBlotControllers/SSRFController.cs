using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace NETMVCBlot.Controllers
{
    public class SSRFController : Controller
    {
        // GET: SSRF
        public ActionResult Index(string input)
        {
            string s = String.Empty;
            // Validate and whitelist the input to prevent SSRF and HPP
            Uri validatedUri;
            if (!Uri.TryCreate(input, UriKind.Absolute, out validatedUri) ||
                (validatedUri.Scheme != Uri.UriSchemeHttp && validatedUri.Scheme != Uri.UriSchemeHttps) ||
                !validatedUri.Host.EndsWith("example.com")) // Replace with your allowed domain
            {
                ViewBag.StringDownloaded = "Invalid or untrusted URL.";
            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    s = wc.DownloadString(validatedUri);
                    ViewBag.StringDownloaded = s;
                }

                var wc2 = new WebClient { BaseAddress = validatedUri.ToString() };

                var wc3 = new WebClient { BaseAddress = "https://www.target.com/i/" + HttpUtility.UrlEncode(input) };

                var hc = new HttpClient { BaseAddress = validatedUri };

                var hc2 = new HttpClient { BaseAddress = new Uri("https://www.target.com/i/" + HttpUtility.UrlEncode(input)) };
            }


            return View();
        }
    }
}