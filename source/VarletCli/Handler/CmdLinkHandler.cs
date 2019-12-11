using McMaster.Extensions.CommandLineUtils;

namespace VarletCli
{
    [Command("link", Description = "Create virtualhost from any directory")]
    public class CmdLinkHandler
    {
        private void OnExecute(IConsole console)
        {
            console.WriteLine($"\nThis is link command\n");
        }
    }
}
