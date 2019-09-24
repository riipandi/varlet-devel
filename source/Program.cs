using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace VarletCli
{
    /**
      * Credit:
      *  https://samyn.co/post/structuring-neat-net-core-console-apps/
     */
    public class Program
    {
        public static int Main(string[] args)
        {
            var options = CommandLineOptions.Parse(args);

            if (options?.Command == null)
            {
                // RootCommand will have printed help
                return 1;
            }

            return options.Command.Run();

        }
    }
}
