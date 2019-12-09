using System;
using Serilog;

namespace VarletCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(@"tmp\varlet.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Hello, world!");

            var a = 10;
            var b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            } catch (Exception ex)  {
                Log.Error(ex, "Something went wrong");
            }
            Log.CloseAndFlush();
            Console.ReadKey();
        }
    }
}
