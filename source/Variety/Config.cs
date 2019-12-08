using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Variety
{
    public class Config
    {
        public static string SelectedPhpVersion { get; set; }
        public static bool InstallHttpService { get; set; }
        public static bool InstallSmtpService { get; set; }
        public static bool CloseMinimizeToTray { get; set; }

        static Config()
        {
            SelectedPhpVersion = "php-7.3-ts";
            InstallHttpService = true;
            InstallSmtpService = true;
            CloseMinimizeToTray = true;
        }

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
