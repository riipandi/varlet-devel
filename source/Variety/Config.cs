using System.IO;
using System.Text;
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

        public static void Initialize(string configFile)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw)) {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("SelectedPhpVersion");
                writer.WriteValue(SelectedPhpVersion);
                writer.WritePropertyName("InstallHttpService");
                writer.WriteValue(InstallHttpService);
                writer.WritePropertyName("InstallSmtpService");
                writer.WriteValue(InstallSmtpService);
                writer.WritePropertyName("CloseMinimizeToTray");
                writer.WriteValue(CloseMinimizeToTray);
                writer.WriteEndObject();
            }

            File.WriteAllText(configFile, sb.ToString());
        }

        public static string Get(string key)
        {
            var o = JObject.Parse(File.ReadAllText(Globals.AppConfigFile));
            return (string)o[key];
        }

        public static void Set(string key, string value)
        {
            var o = JObject.Parse(File.ReadAllText(Globals.AppConfigFile));
            return;
        }
    }
}
