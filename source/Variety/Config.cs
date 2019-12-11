using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Variety
{
    public class Config
    {
        private static string SelectedPhpVersion { get; set; }
        private static DateTime LastUpdateCheck { get; set; }
        private static bool CloseMinimizeToTray { get; set; }
        private static string[] Services { get; set; }

        static Config()
        {
            SelectedPhpVersion = "php-7.3-ts";
            LastUpdateCheck = DateTime.Now;
            CloseMinimizeToTray = true;
            Services = new string[] {"http"};
        }
        
        public static void Initialize()
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw)) {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("SelectedPhpVersion");
                writer.WriteValue(SelectedPhpVersion);
                writer.WritePropertyName("LastUpdateCheck");
                writer.WriteValue(LastUpdateCheck);
                writer.WritePropertyName("CloseMinimizeToTray");
                writer.WriteValue(CloseMinimizeToTray);
                writer.WriteEndObject();
            }
            File.WriteAllText(References.AppConfigFile, sb.ToString());
        }
        
        public static string Get(string key)
        {
            var o = JObject.Parse(File.ReadAllText(References.AppConfigFile));
            return (string)o[key];
        }
    }
}