using System.IO;
using Newtonsoft.Json;

namespace Variety
{
    public class Config
    {
        public string ServerHost { get; set; }
        public string ServerPort { get; set; }
        public string ServerTimeout { get; set; }
        
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
            // var jsonValue = File.ReadAllText(configFile);
            var jsonValue = @"{ 
			    ServerHost: null, 
				ServerPort: null, 
				ServerTimeout: null
            }";

            return JsonConvert.DeserializeObject<Config>(jsonValue);
        }
    }
}
