using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;

namespace DemoApi
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var logger = new LoggerConfiguration()
            .Filter.ByExcluding(Matching.FromSource("Microsoft"))
            .WriteTo.Console(outputTemplate: "{Message}{NewLine}", theme: ConsoleTheme.None)
            .CreateLogger();

         // TODO: Move to bivouac
         var host = new WebHostBuilder()
            .ConfigureAppConfiguration((context, configurationBuilder) =>
            {
               configurationBuilder
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true);
            })
            .UseUrls("http://*:8090/")
            .UseKestrel()
            .UseStartup<Startup>()
            .UseSerilog(logger)
            .Build();

         host.Run();
      }
   }
}