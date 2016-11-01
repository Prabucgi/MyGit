using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiLanguageWithDialectMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["LanguageCode"];
            var languageCode = "en";
            if (cookie?.Value != null)
            {
                languageCode = cookie.Value;
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
        }
    }
}
