using System.Diagnostics;
using System.Reflection;

namespace Variety
{
    public static class Globals
    {
        public static string DefaultPhpVersion { get; set; }

        public static string ServiceNameHttp { get; }
        private static int LastUpdateCheck { get; set; }

        static Globals()
        {
            DefaultPhpVersion = "7.2";
            ServiceNameHttp = "VarletHttpd";
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
