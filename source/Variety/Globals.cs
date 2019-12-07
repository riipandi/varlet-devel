using System.Diagnostics;
using System.Reflection;

namespace Variety
{
    public static class Globals
    {
        public static string DefaultPhpVersion { get; set; }

        public static string ServiceNameHttp { get; }
        public static string ServiceNameSmtp { get; }
        private static int LastUpdateCheck { get; set; }

        static Globals()
        {
            DefaultPhpVersion = "php-7.3-ts";
            ServiceNameHttp = "VarletHttpd";
            ServiceNameSmtp = "VarletMailhog";
            LastUpdateCheck = 5;
        }

        public static string Version
        {
            get
            {
                var asm = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                return $"{fvi.ProductMajorPart}.{fvi.ProductMinorPart}.{fvi.ProductBuildPart}";
            }
        }

        public static string ConfigFileName()
        {
            return "varlet.json";
        }
    }
}
