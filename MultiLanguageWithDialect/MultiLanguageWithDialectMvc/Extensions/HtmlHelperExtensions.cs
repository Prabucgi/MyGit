using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MultiLanguageWithDialectMvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString GetHiddenInputWithResourceJson(this HtmlHelper htmlHelper, string identifier, List<string> keys)
        {
            return GetHiddenInputWithResourceJson(htmlHelper, Resources.Default.ResourceManager, identifier, keys);
        }

        public static HtmlString GetHiddenInputWithResourceJson(this HtmlHelper htmlHelper, ResourceManager resourceManager, string identifier, List<string> keys)
        {
            if (!keys.Any()) return new HtmlString("");

            var dictionary = new Dictionary<string, string>();

            keys.ForEach(key =>
            {
                dictionary.Add(key, GetText(htmlHelper, resourceManager, key));
            });

            var json = JsonConvert.SerializeObject(dictionary);

            return new HtmlString($"<input type='hidden' id='{identifier}' value='{json}'");
        }

        public static string GetText(this HtmlHelper htmlHelper, string key)
        {
            return GetText(htmlHelper, Resources.Default.ResourceManager, key);
        }

        public static string GetText(this HtmlHelper htmlHelper, ResourceManager resourceManager, string key)
        {
            return resourceManager.GetDialectOrDefault(key, "ABC");
        }
    }
}
