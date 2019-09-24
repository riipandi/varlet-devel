using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

using System.Runtime.InteropServices;
using System.Security.Principal;

namespace VarletCli
{
    public class Program
    {
        public static int Main(string[] args)
        {
            RequireAdministrator();

            // Return the result
            var options = CommandLineOptions.Parse(args);

            if (options?.Command == null)
            {
                // RootCommand will have printed help
                return 1;
            }

            return options.Command.Run();
        }

        [DllImport("libc")]
        public static extern uint getuid();

        /// <summary>
        /// Asks for administrator privileges upgrade if the platform supports it, otherwise does nothing
        /// </summary>
        public static void RequireAdministrator()
        {
            string name = System.AppDomain.CurrentDomain.FriendlyName;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                    {
                        string message = "\r\nApplication must be run as administrator!\r\n";
                        // throw new InvalidOperationException(message);
                        Console.WriteLine(message);
                        System.Environment.Exit(0);
                    }
                }
            }
            else if (getuid() != 0)
            {
                string message = "\r\nApplication must be run as root/sudo!\r\n";

                Console.WriteLine(message);

                // throw new InvalidOperationException(message);

                System.Environment.Exit(0);
            }
        }
    }
}
