using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace CheatCodes.Search
{
  public class Program
  {
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", false, true)
      .AddEnvironmentVariables()
      .Build();

    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .WriteTo.File(new JsonFormatter(), @"c:\temp\logs\cheatcodes_search_api_log.json", shared: true)
        .CreateLogger();


      try
      {
        Log.Information("Starting web host search");
        var host = CreateHostBuilder(args).Build();
        host.Run();
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }




    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
        .UseSerilog();

    }

  }
}