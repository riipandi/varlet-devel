using System;
using System.Collections.Generic;
using Variety;
using CommandLine;

namespace VarletCli
{
    class DefaultOptions { }

    [Verb("add", HelpText = "Add file contents to the index.")]
    class AddOptions
    {
        [Option('f', "file", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('V', "verbose", Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }
    }

    [Verb("commit", HelpText = "Record changes to the repository.")]
    class CommitOptions {}

    [Verb("clone", HelpText = "Clone a repository into a new directory.")]
    class CloneOptions {}

    class Program
    {
        static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<AddOptions, CommitOptions, CloneOptions>(args)
                .MapResult(
                    (AddOptions opts) => CmdAddHandler(opts),
                    (CommitOptions opts) => CmdCommitHandler(opts),
                    (CloneOptions opts) => CmdCloneHandler(opts),
                    errs => 1);
        }

        private static int CmdAddHandler(AddOptions opts)
        {
            return 1;
        }

        private static int CmdCommitHandler(CommitOptions opts)
        {
            return 1;
        }

        private static int CmdCloneHandler(CloneOptions opts)
        {
            return 1;
        }
    }
}