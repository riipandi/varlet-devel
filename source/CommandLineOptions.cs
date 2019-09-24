using Microsoft.Extensions.CommandLineUtils;
using VarletCli.Commands;
using VarletCli.CommandConfiguration;

namespace VarletCli
{
    public class CommandLineOptions
    {
        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();

            var app = new CommandLineApplication
            {
                Name = "varlet",
                FullName = "\r\nA more enjoyable local development experience for Windows."
            };

            app.HelpOption("-?|-h|--help");

            var forcedOption = app.Option("-f|--force", "Force the operation to run", CommandOptionType.NoValue);

            RootCommandConfiguration.Configure(app, options);

            var result = app.Execute(args);

            if (result != 0)
            {
                return null;
            }

            options.isForced = forcedOption.HasValue();

            return options;
        }

        public ICommand Command { get; set; }
        public bool isForced { get; set; }
    }
}
