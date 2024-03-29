﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

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

        public static void ReplaceStringInFile(string filename, string search, string replace)
        {
            var sr = new StreamReader(filename);
            var rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            var sw = new StreamWriter(filename);
            for (var i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains(search))
                {
                    rows[i] = rows[i].Replace(search, replace);
                }
                sw.WriteLine(rows[i]);
            }
            sw.Close();
        }
    }
}
