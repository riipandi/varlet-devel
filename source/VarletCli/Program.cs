/**
 * Varlet Controller
 * 
 * The following code was provided for controlling Varlet. 
 * 
 * Author Name: Aris Ripandi <aris@ripandi.id>
 * Author Url: https://github.com/riipandi
 * 
 */

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;

namespace VarletCli
{
    static class Program
    {
        private class Options
        {
            [Option('v', "verbose", Default = false, Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
        }
        
        static void Main(string[] args)
        {
            Helper hlp = new Helper();

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.Verbose)
                       {
                           var table = new ConsoleTable("one", "two", "three");
                           table.AddRow(1, 2, 3).AddRow("this line should be longer", "yes it is", "oh");

                           Console.WriteLine("\nFORMAT: Default:\n");
                           table.Write();

                           Console.WriteLine("\nFORMAT: Alternative:\n");
                           table.Write(Format.Alternative);
                           Console.WriteLine();
                       }
                       else
                       {
                           var dirPath = Environment.CurrentDirectory;
                           var dirName = Path.GetFileName(dirPath);
                           hlp.PrintlnInfo("You are here: " + dirName.ToLower());
                           hlp.PrintlnSuccess("Full path is: " + dirPath.Replace("\\", "/"));
                       }
                   });
        }
    }
}
