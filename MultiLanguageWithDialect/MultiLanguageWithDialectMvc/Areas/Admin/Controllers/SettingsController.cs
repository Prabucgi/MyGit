using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MultiLanguageWithDialectMvc.Extensions;
using MultiLanguageWithDialectMvc.Models;

namespace MultiLanguageWithDialectMvc.Areas.Admin.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index(string code = null)
        {
            code = code ?? Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var langauges = new List<Language>()
            {
                new Language() {Value = "en", Text = "English"},
                new Language() {Value = "sv", Text = "Swedish"}
            };
            ViewBag.Langauges = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "English", Value = "en", Selected = code == "en"},
                new SelectListItem() {Text = "Swedish", Value = "sv", Selected = code == "sv"}
            };
            List<ResourceKey> keys = Resources.Default.ResourceManager.GetResourceKeys(new CultureInfo(code), "ABC");
            var vm = new DialectViewModel() { CurrentLanguageCode = code, ResourceKeys = keys };
            return View(vm);
        }

        public ActionResult SaveResources(DialectViewModel viewModel)
        {
            //var keys = Resources.Default.ResourceManager.GetResourceKeys(new CultureInfo(code), "ABC");
            return RedirectToAction("Index", new { code = viewModel.CurrentLanguageCode });
        }

    }
}