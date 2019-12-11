using McMaster.Extensions.CommandLineUtils;
using Variety;

namespace VarletCli
{
    [Command("install", Description = "Install Varlet services")]
    public class CmdInstallHandler
    {
        private void OnExecute(IConsole console)
        {
            console.WriteLine($"\nThis is install command\n");
        }
    }
}
