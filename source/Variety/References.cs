using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using static System.String;

namespace Variety
{
    public static class References
    {
        private static string AppRegistryPath { get; }
        private const string AppConfigFileName = "varlet.json";
        public const string ServiceNameHttp = "VarletHttpd";
        public const string ServiceNameSmtp = "VarletMailhog";

        static References()
        {
            AppRegistryPath = @"HKLM\Software\Aris Ripandi\Varlet";
        }

        public static string AppConfigFile
        {
            get {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return path + @"\" + AppConfigFileName;
            }
        }

        public static string AppLogFile
        {
            get {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return path + @"\varlet.log";
            }
        }

        public static string AppVersionSemantic
        {
            get {
                var asm = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                return $"{fvi.ProductMajorPart}.{fvi.ProductMinorPart}.{fvi.ProductBuildPart}";
            }
        }

        public static string AppBuildNumber
        {
            get {
                var asm = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                return $"{fvi.ProductPrivatePart}";
            }
        }

        public static string AppRootPath(string path)
        {
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!IsNullOrEmpty(path))  {
                return appPath + path;
            }
            return appPath;
        }

        public static string WwwDirectory
        {
            get {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return AppRootPath(@"\www");
            }
        }

        public static string ProgramFilesDir(string path)
        {
            if( 8 == IntPtr.Size  || (!IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))  {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            if (string.IsNullOrEmpty(path)) {
                return Environment.GetEnvironmentVariable("ProgramFiles") + path;
            } else {
                return Environment.GetEnvironmentVariable("ProgramFiles");
            }
        }
    }
}
