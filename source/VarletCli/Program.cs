using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace VarletCli
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            // Log.Logger = new LoggerConfiguration()
            //     .MinimumLevel.Debug()
            //     .WriteTo.Console()
            //     .WriteTo.File(References.AppLogFile, rollingInterval: RollingInterval.Day)
            //     .CreateLogger();
            // try
            // {

            // } catch (Exception ex)  {
            //     Log.Error(ex, "Something went wrong");
            // }
            // Log.CloseAndFlush();
            
            var app = new CommandLineApplication
            {
                Name = "varlet",
                Description = "Varlet is minimalism web development stack",
            };

            app.HelpOption(true);
            app.Command("config", configCmd =>
            {
                configCmd.OnExecute(() =>
                {
                    Console.WriteLine("Specify a subcommand");
                    configCmd.ShowHelp();
                    return 1;
                });

                configCmd.Command("set", setCmd =>
                {
                    setCmd.Description = "Set config value";
                    var key = setCmd.Argument("key", "Name of the config").IsRequired();
                    var val = setCmd.Argument("value", "Value of the config").IsRequired();
                    setCmd.OnExecute(() =>
                    {
                        Console.WriteLine($"Setting config {key.Value} = {val.Value}");
                    });
                });

                configCmd.Command("list", listCmd =>
                {
                    var json = listCmd.Option("--json", "Json output", CommandOptionType.NoValue);
                    listCmd.OnExecute(() =>  {
                        Console.WriteLine(json.HasValue() ? "{\"dummy\": \"value\"}" : "dummy = value");
                    });
                });
                
                configCmd.Command("table", tableCmd =>
                {
                    tableCmd.OnExecute(() =>  {
                        var table = new ConsoleTable("one", "two", "three");
                        table.AddRow(1, 2, 3).AddRow("this line should be longer", "yes it is", "oh");

                        Console.WriteLine("\nFORMAT: Default:\n");
                        table.Write();

                        Console.WriteLine("\nFORMAT: Alternative:\n");
                        table.Write(Format.Alternative);
                        Console.WriteLine();
                    });
                });
                
                configCmd.Command("dir", dirCmd =>
                {
                    dirCmd.Description = "Get directory path";
                    
                    dirCmd.OnExecute(() =>  {
                        var dirPath = Environment.CurrentDirectory;
                        var dirName = Path.GetFileName(dirPath);
                        Console.WriteLine("You are here: " + dirName.ToLower());
                        Console.WriteLine("Full path is: " + dirPath.Replace("\\", "/"));
                    });
                });
            });

            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a subcommand");
                app.ShowHelp();
                return 1;
            });

            return app.Execute(args);
        }
    }
}