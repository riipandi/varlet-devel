using System;
using Serilog;
using McMaster.Extensions.CommandLineUtils;
using Variety;

namespace VarletCli
{
    public class Program
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

            var app = new CommandLineApplication();
            app.HelpOption();
            var optionSubject = app.Option("-s|--subject <SUBJECT>", "The subject", CommandOptionType.SingleValue);
            var optionRepeat = app.Option<int>("-n|--count <N>", "Repeat", CommandOptionType.SingleValue);
            app.OnExecute(() =>
            {
                var subject = optionSubject.HasValue()  ? optionSubject.Value()  : "world";
                var count = optionRepeat.HasValue() ? optionRepeat.ParsedValue : 1;
                for (var i = 0; i < count; i++)  {
                    // Log.Information($"Hello {subject}!");
                    Console.WriteLine($"Hello {subject}!");
                }
                return 0;
            });

            return app.Execute(args);
        }
    }
}
