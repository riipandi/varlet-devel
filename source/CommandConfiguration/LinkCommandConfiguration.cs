using System;
using System.Collections.Generic;
using System.Text;
using VarletCli.Commands;
using Microsoft.Extensions.CommandLineUtils;

namespace VarletCli.CommandConfiguration
{
    public static class LinkCommandConfiguration
    {
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {

            command.Description = "Create virtualhost and serving the site";
            command.HelpOption("--help|-h|-?");

            var domainArgument = command.Argument("domain", "Name I should say hello to");

            command.OnExecute(() =>
            {
                options.Command = new LinkCommand(domainArgument.Value, options);

                return 0;
            });

        }
    }
}
