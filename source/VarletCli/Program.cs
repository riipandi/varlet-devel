using McMaster.Extensions.CommandLineUtils;

namespace VarletCli
{
    [Command(
        Name = "varlet",
        Description = "Varlet is minimalism web development stack"),
        Subcommand(typeof(CmdSiteManager), typeof(CmdCreateApp)
    )]
    class Program
    {
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify a subcommand.");
            app.ShowHelp();
            return 1;
        }
    }
}
