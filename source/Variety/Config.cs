using System.IO;
using Newtonsoft.Json;

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

        public void Update(string configFile)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(configFile, json);
        }

        public static Config Load()
        {
            string jsonValue;
            
            // var jsonValue = File.ReadAllText(configFileName);
            
            jsonValue = @"{
                PhpVersion: '7.3',
                InstallHttpService: true,
                InstalMailhogService: false,
            }";

            return JsonConvert.DeserializeObject<Config>(jsonValue);
        }
    }
}