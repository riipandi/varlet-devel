using System;
using System.Globalization;
using System.IO;
using System.Text;
using IniParser;
using IniParser.Model;

namespace Variety
{
    public static class Config
    {
        private static string SelectedPhpVersion { get; }
        public static string DocumentRoot { get; }
        public static string VhostExtension { get; }
        private static DateTime LastUpdateCheck { get; set; }
        private static bool CloseMinimizeToTray { get; }
        private static string[] Services { get; }

        static Config()
        {
            SelectedPhpVersion = "php-7.3-ts";
            LastUpdateCheck = DateTime.Now;
            CloseMinimizeToTray = true;
            VhostExtension = ".test";
            DocumentRoot = References.AppRootPath(@"\www");
            Services = new[] {"http"};
        }

        public static void Initialize()
        {
            var data = new IniData();

            data["App"]["LastUpdateCheck"] = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            data["App"]["CloseMinimizeToTray"] = true.ToString();
            data["App"]["DocumentRoot"] = DocumentRoot;
            data["App"]["SelectedPhpVersion"] = SelectedPhpVersion;
            data["App"]["VhostExtension"] = VhostExtension;

            File.WriteAllText(References.AppConfigFile, data.ToString());
        }

        public static string Get(string section, string key)
        {
            var parser = new FileIniDataParser();
            var cfg = parser.ReadFile(References.AppConfigFile);

            return cfg[section][key];
        }

        public static void Set(string section, string key, string value)
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(References.AppConfigFile);
            data[section][key] = value;
            parser.WriteFile(References.AppConfigFile, data);
        }
    }
}
