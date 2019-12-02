using System;
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
                           Console.WriteLine("Quick start example!");
                       }
                   });
        }
    }
}
