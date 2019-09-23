using Microsoft.Extensions.CommandLineUtils;

namespace VarletCli.Commands
{

    public class RootCommand : ICommand
    {
        private readonly CommandLineApplication _app;

        public RootCommand(CommandLineApplication app)
        {
            _app = app;
        }

        public int Run()
        {
            _app.ShowHelp();

            return 1;
        }

    }

}
