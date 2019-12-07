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

        public void Save(string configFile)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(configFile, json);
        }

        public static void Update(string channel, string key)
        {
            var f = File.ReadAllText(Globals.AppConfigFile);
            var o = JObject.Parse(f);
            var c = (JObject)o[channel];
            c[key] = ((string)c[key]).ToUpper();
        }

        public static string Get(string channel, string key)
        {
            var f = File.ReadAllText(Globals.AppConfigFile);
            var o = JObject.Parse(f);
            var c = (JObject)o[channel];
            return (string)c[key];
        }

        public static Config Load()
        {
            const string jsonValue = @"{
                PhpVersion: '7.3',
                InstallHttpService: true,
                InstalMailhogService: false,
            }";

            return JsonConvert.DeserializeObject<Config>(jsonValue);
        }
    }
}
