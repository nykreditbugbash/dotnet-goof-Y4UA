using NETMVCBlot.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETMVCBlot.Controllers
{
    public class CallbackController : BaseController
    {
        protected override void Utility()
        {

        }

        [HttpPost]
        public ActionResult Download(string fileName)
        {
            if (!IBValidator.IsValidFileName(fileName))
                return new HttpNotFoundResult();

            // Prevent directory traversal by getting only the file name
            var safeFileName = System.IO.Path.GetFileName(fileName);
            var filePath = System.IO.Path.Combine(@"D:\wwwroot\reports\", safeFileName);

            if (!System.IO.File.Exists(filePath))
                return new HttpNotFoundResult();

            return new FilePathResult(filePath, "application/pdf");
        }

        public String DownloadAsString(string fileName)
        {
            // Prevent directory traversal by getting only the file name
            var safeFileName = System.IO.Path.GetFileName(fileName);
            var filePath = System.IO.Path.Combine(@"D:\wwwroot\reports\", safeFileName);

            if (!System.IO.File.Exists(filePath))
                return null;

            return System.IO.File.ReadAllText(filePath);
        }

        public JsonResult ExecuteProcess(string argument)
        {
            // Prevent OS command injection by allowing only safe arguments (e.g., IP addresses or hostnames)
            if (string.IsNullOrWhiteSpace(argument) || argument.Any(c => !char.IsLetterOrDigit(c) && c != '.' && c != '-'))
            {
                return Json(new { error = "Invalid argument" }, JsonRequestBehavior.AllowGet);
            }
            Process.Start("cmd.exe", "/C ping.exe " + argument);
            return null;
        }
    }
}