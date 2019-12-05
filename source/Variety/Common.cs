using System;
using System.Diagnostics;

namespace Variety
{
    public class Common
    {
        /// <summary>
        /// Print text with color
        /// </summary>
        public void PrintSuccess(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(value);
            Console.ResetColor();
        }

        public void PrintInfo(string value)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(value);
            Console.ResetColor();
        }

        public void PrintWarning(string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value);
            Console.ResetColor();
        }

        public void PrintError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Print line with color
        /// </summary>
        public void PrintlnSuccess(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        public void PrintlnInfo(string value)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        public void PrintlnWarning(string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        public void PrintlnError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

         public static void OpenUrl(string url)
         {
             try {
                 Process.Start(url);
             } catch (FormatException) {
                 // do something here
             }
         }

         public static void OpenWithNotepad(string file, bool asAdmin)
         {
             try {
                 var p = new Process {StartInfo = new ProcessStartInfo() {UseShellExecute = true, FileName = file}};
                 p.Start();
                 p.WaitForExit();
             } catch (FormatException) {
                 // do something here
             }
         }
    }
}
