using System;
using System.Diagnostics;

namespace Variety
{
    public class Utilities
    {
        public static void OpenWithNotepad(string file, bool runas = false)
        {
            try {
                var proc = new Process {StartInfo =
                {
                    FileName = "notepad.exe",
                    Arguments = file,
                    UseShellExecute = true
                }};
                if (runas == true) proc.StartInfo.Verb = "runas";
                proc.Start();
            } catch (FormatException) {}
        }
    }
}