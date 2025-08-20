using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NETMVCBlot.Controllers
{
    public class StreamWriterRelatedCasesController : Controller
    {
        public ActionResult Index(string input)
        {            
            // Sanitize input to prevent HTTP Parameter Pollution and SSRF
            if (string.IsNullOrWhiteSpace(input) || input.Contains("&") || input.Contains("=") || input.Contains("http"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid input.");
            }

            var req = (HttpWebRequest)WebRequest.Create("https://www.codethreat.com/index/");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            string safeParam = HttpUtility.UrlEncode(input);
            string formContent = $"cmd=doSale&param={safeParam}";

            using var sw = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
            sw.Write(formContent);

            string response = null;
            using (var sr = new StreamReader(req.GetResponse().GetResponseStream()))
                response = sr.ReadToEnd();

            string formContent2 = $"cmd=doSale&param={safeParam}";
            var req2 = (HttpWebRequest)WebRequest.Create("https://www.codethreat.com/index/?" + formContent2);
            req2.Method = "GET";

            string response2 = null;
            using (var sr = new StreamReader(req2.GetResponse().GetResponseStream()))
                response2 = sr.ReadToEnd();

            // SSRF FIX: Do not allow user input as URL
            // string response3 = null;
            // using (var sr = new StreamReader(req3.GetResponse().GetResponseStream()))
            //     response3 = sr.ReadToEnd();

            // Insecure File Upload FIX: Do not write to files based on user input
            // using (var sw = new StreamWriter(input))
            //     sw.Write(formContent4);

            // Command Injection FIX: Do not pass user input to command line
            // using var process = new System.Diagnostics.Process();
            // process.StartInfo.RedirectStandardInput = true;
            // process.Start();
            // using (StreamWriter writer = process.StandardInput)
            // {
            //     writer.WriteLine(input);
            // }

            return View();
        }
    }
}