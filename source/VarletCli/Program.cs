using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Semver;

namespace VarletCli
{
    static class Program
    {
        public static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<CommandAddOptions, CommandGetPathOptions, CommandTableOptions, CommandUpdateOptions>(args)
                .MapResult(
                    (CommandAddOptions opts) => CommandAddHandler(opts),
                    (CommandGetPathOptions opts) => CommandGetPathHandler(opts),
                    (CommandTableOptions opts) => CommandTableHandler(opts),
                    (CommandUpdateOptions opts) => CommandUpdateHandler(opts),
                    errs => 1);
        }

        private static int CommandAddHandler(CommandAddOptions opts)
        {
            Console.WriteLine("CommandAddHandler " + opts.InputFiles);
            Task.Delay(TimeSpan.FromSeconds(1));
            return 1;
        }

        private static int CommandGetPathHandler(CommandGetPathOptions opts)
        {
            var dirPath = Environment.CurrentDirectory;
            var dirName = Path.GetFileName(dirPath);
            Console.WriteLine("You are here: " + dirName.ToLower());
            Console.WriteLine("Full path is: " + dirPath.Replace("\\", "/"));
            return 1;
        }

        private static int CommandUpdateHandler(CommandUpdateOptions opts)
        {
            var version = SemVersion.Parse("1.1.0");

            Console.WriteLine("You are using version {0}", version);

            if (version >= "1.0.0")
            {
                Console.WriteLine("Up to date!");
            }
            else
            {
                Console.WriteLine("Too old!");
            }


            return 1;
        }

        private static int CommandTableHandler(CommandTableOptions opts)
        {
            var table = new ConsoleTable("one", "two", "three");
            table.AddRow(1, 2, 3).AddRow("this line should be longer", "yes it is", "oh");

            Console.WriteLine("\nFORMAT: Default:\n");
            table.Write();

            Console.WriteLine("\nFORMAT: Alternative:\n");
            table.Write(Format.Alternative);
            Console.WriteLine();
            return 1;
        }
    }

    [Verb("add", HelpText = "Add file contents to the index.")]
    internal class CommandAddOptions
    {
        [Option('f', "file", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('V', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }
    }

    [Verb("table", HelpText = "Example ConsoleTable.")]
    internal class CommandTableOptions {}

    [Verb("getpath", HelpText = "Get current path.")]
    internal class CommandGetPathOptions {}

    [Verb("update", HelpText = "Check for updates.")]
    internal class CommandUpdateOptions {}
}
