using System;
using Variety;
using CommandLine;
using System.Collections.Generic;

namespace VarletCli
{
    class Program
    {
        class Options
        {
            [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
            public IEnumerable<string> InputFiles { get; set; }

            // Omitting long name, defaults to name of property, ie "--verbose"
            [Option(Default = false, HelpText = "Prints all messages to standard output.")]
            public bool Verbose { get; set; }

            [Option("stdin", Default = false, HelpText = "Read from stdin")]
            public bool stdin { get; set; }

            [Value(0, MetaName = "offset", HelpText = "File offset.")]
            public long? Offset { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
              .WithNotParsed<Options>((errs) => HandleParseError(errs));
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("error: " + errs);
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            Console.WriteLine("handle: " + opts);
        }
    }
}
