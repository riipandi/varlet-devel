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

            var enthousiasticSwitch = app.Option("-e|--enthousiastically",
                                          "Whether the app should be enthousiastic.",
                                          CommandOptionType.NoValue);

            RootCommandConfiguration.Configure(app, options);

            var result = app.Execute(args);

            if (result != 0)
            {
                return null;
            }

            options.IsEnthousiastic = enthousiasticSwitch.HasValue();

            return options;
        }

        public ICommand Command { get; set; }
        public bool IsEnthousiastic { get; set; }
    }
}
