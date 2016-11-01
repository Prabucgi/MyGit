using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MultiLanguageWithDialectMvc.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(string LanguageCode)
        {
            if (!string.IsNullOrWhiteSpace(LanguageCode))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageCode);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageCode);
            }
            var cookie = new HttpCookie("LanguageCode", LanguageCode);
            Response.Cookies.Add(cookie);
            return View("Index");
        }
    }
}