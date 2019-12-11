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
            var fs = new FileStream(defaultVhostFile, FileMode.Create, FileAccess.Write);
            new StreamWriter(fs).Write("");
            fs.Close();

            File.WriteAllText(defaultVhostFile, tplVhostFile);
            Utilities.ReplaceStringInFile(defaultVhostFile, "<<SITENAME>>", "localhost");
            Utilities.ReplaceStringInFile(defaultVhostFile, "<<SITEROOT>>", Config.Get("App", "DocumentRoot").Replace("\\", "/"));
        }
    }
}
