using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Variety
{
    public class Config
    {
        public string PhpVersion { get; set; }
        public bool InstallHttpService { get; set; }
        public bool InstalMailhogService { get; set; }

        public static void Initialize()
        {
            // do something
        }

        public static string Get(string channel, string key)
        {
            var f = File.ReadAllText(Globals.AppConfigFile);
            var o = JObject.Parse(f);
            var c = (JObject)o[channel];
            return (string)c[key];
        }

        public static void Set(string channel, string key, string value)
        {
            var f = File.ReadAllText(Globals.AppConfigFile);
            var o = JObject.Parse(f);
            var c = (JObject)o[channel];
            c[key] = ((string)c[value]);
        }
    }
}
