using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLanguageWithDialectMvc.Models
{
    public class Dialect
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ResourceKey
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Dialect { get; set; }
    }

    public class Language
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class DialectViewModel
    {
        public DialectViewModel()
        {
            ResourceKeys = new List<ResourceKey>();
        }
        public string CurrentLanguageCode { get; set; }

        public List<ResourceKey> ResourceKeys { get; set; }
    }
}