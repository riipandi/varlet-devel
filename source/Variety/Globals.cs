namespace Variety
{
    public static class Globals
    {
        public static string DefaultPhpVersion { get; set; }

        public static string ServiceNameHttp { get; }
        public static int LastUpdateCheck { get; set; }

        static Globals()
        {
            DefaultPhpVersion = "7.2";
            ServiceNameHttp = "VarletHttpd";
            LastUpdateCheck = 5;
        }

        public static string ConfigFileName()
        {
            return "varlet.json";
        }
    }
}