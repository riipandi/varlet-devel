using McMaster.Extensions.CommandLineUtils;
using Variety;

namespace VarletCli.Handler
{
    [Command("version", Description = "Print Varlet version information")]
    public class CmdVersionHandler
    {
        private void OnExecute(IConsole console)
        {
            console.WriteLine($"\nYou are using Varlet version {References.AppVersionSemantic} build {References.AppBuildNumber}\n");
        }
    }
}
