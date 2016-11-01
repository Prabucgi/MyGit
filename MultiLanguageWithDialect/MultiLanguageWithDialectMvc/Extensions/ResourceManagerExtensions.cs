using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web.Helpers;
using MultiLanguageWithDialectMvc.Models;

namespace MultiLanguageWithDialectMvc.Extensions
{
    public static class ResourceManagerExtensions
    {
        public static string GetDialectOrDefault(this ResourceManager manager, string key, string customer)
        {
            if (string.IsNullOrWhiteSpace(key)) return "";

            string value = manager.GetString(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                return $"<<Key '{key}' missing>>";
            }

            var allKeys = manager.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true).OfType<DictionaryEntry>();
            var dialectKey = allKeys.FirstOrDefault(i => i.Key.ToString().EndsWith("_dialect"));
            if (dialectKey.Key != null)
            {
                dynamic data = Json.Decode(dialectKey.Value.ToString());
                foreach (var item in data)
                {
                    if (item.customer != customer) continue;
                    var dialect = item.keys[key];
                    if (!string.IsNullOrWhiteSpace(dialect))
                        return dialect;
                }
            }
            return value;
        }

        public static List<ResourceKey> GetResourceKeys(this ResourceManager manager, CultureInfo culture, string customer)
        {
            var keys = new List<ResourceKey>();
            var allKeys = manager.GetResourceSet(culture, true, true).OfType<DictionaryEntry>().ToList();
            var dialectKey = allKeys.FirstOrDefault(i => i.Key.ToString().EndsWith("_dialect"));
            allKeys.ForEach(item =>
            {
                if (item.Key.ToString().EndsWith("_dialect") || item.Key.ToString().EndsWith("Format")) return;
                keys.Add(new ResourceKey()
                {
                    Name = item.Key.ToString(),
                    Value = item.Value.ToString(),
                    Dialect = DialectOrEmpty(dialectKey, item.Key.ToString(), customer)
                });
            });

            return keys;
        }

        private static string DialectOrEmpty(DictionaryEntry dialectKey, string key, string customer)
        {
            if (dialectKey.Key == null) return "";
            dynamic data = Json.Decode(dialectKey.Value.ToString());
            foreach (var item in data)
            {
                if (item.customer != customer) continue;
                var dialect = item.keys[key];
                if (!string.IsNullOrWhiteSpace(dialect))
                    return dialect;
            }
            return "";
        }
    }
}
