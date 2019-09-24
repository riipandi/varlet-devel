using System;
using Microsoft.Extensions.CommandLineUtils;

namespace VarletCli.Commands
{

    public class LinkCommand : ICommand
    {

        private readonly string _name;
        private readonly CommandLineOptions _options;

        public LinkCommand(string name, CommandLineOptions options)
        {
            _name = name;
            _options = options;
        }

        public int Run()
        {
            Console.WriteLine("Hey "
                + (_name != null ? _name : "World")
                + (_options.isForced ? "!!!" : "."));

            return 0;
        }

    }

}
