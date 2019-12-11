using McMaster.Extensions.CommandLineUtils;
using Variety;

namespace VarletCli
{
    [Command("version", Description = "Print Varlet version information")]
    public class CmdVersionHandler
    {
        [Option("-s")]
        private bool Semantic { get; }
        private void OnExecute(IConsole console)
        {
            console.WriteLine(Semantic
                ? $"{References.AppVersionSemantic}"
                : $"\nYou are using Varlet version {References.AppVersionSemantic} build {References.AppBuildNumber}\n");
        }
    }
}
