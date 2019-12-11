using System.IO;

namespace Variety
{
    public static class VirtualHost
    {
        private static readonly string ApacheConfDir = References.AppRootPath(@"\pkg\httpd\conf");

        public static void SetDefaultVhost()
        {
            var tplVhostFile = References.GetEmbeddedResourceContent("VarletUi.vhost.tpl.conf");
            var defaultVhostFile = ApacheConfDir + @"\vhost\00-default.conf";

            if (File.Exists(defaultVhostFile)) File.Delete(defaultVhostFile);
            var fs1 = new FileStream(defaultVhostFile, FileMode.Create, FileAccess.Write);
            new StreamWriter(fs1).Write("");
            fs1.Close();

            File.WriteAllText(defaultVhostFile, tplVhostFile);
            Utilities.ReplaceStringInFile(defaultVhostFile, "<<SITENAME>>", "localhost");
            Utilities.ReplaceStringInFile(defaultVhostFile, "<<SITEROOT>>", Config.Get("App", "DocumentRoot").Replace("\\", "/"));
            Utilities.ReplaceStringInFile(defaultVhostFile, "<<PORT_HTTP>>", Config.Get("App", "DefaultPortHttp"));
            Utilities.ReplaceStringInFile(defaultVhostFile, "<<PORT_HTTPS>>", Config.Get("App", "DefaultPortHttps"));

            // Don't forget to change default port
            var cfgPortsFile = ApacheConfDir + @"\ports.conf";
            if (File.Exists(cfgPortsFile)) File.Delete(cfgPortsFile);
            var fs2 = new FileStream(cfgPortsFile, FileMode.Create, FileAccess.Write);
            new StreamWriter(fs2).Write("");
            fs2.Close();
            var portHttp = Config.Get("App", "DefaultPortHttp");
            var portHttps = Config.Get("App", "DefaultPortHttps");
            var cfgPorts = "Listen "+portHttp+"\n<IfModule ssl_module>\n\tListen "+portHttps+"\n</IfModule>";
            File.WriteAllText(cfgPortsFile, cfgPorts);
        }
    }
}
