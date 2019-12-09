using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Variety
{
  public class References
  {
      private static string AppConfigFileName { get; }

      static References()
      {
          AppConfigFileName = "varlet.json";
      }

      public static string AppConfigFile
      {
          get {
              var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
              return path + @"\" + AppConfigFileName;
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

      public static string WwwDirectory
      {
          get {
              var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
              return path + @"\www";
          }
      }
  }
}
