using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace Api
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
        .WriteTo.File(new JsonFormatter(), @"c:\temp\logs\cheatcodes_api_log.json", shared: true)
        .WriteTo.Console()
        .CreateLogger();

      try
      {
        Log.Information("Starting web host");
        CreateHostBuilder(args).Build().Run();
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

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .ConfigureKestrel((context, options) =>
        {
          options.AddServerHeader = false;
        })
        .UseSerilog();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.ConfigureKestrel(serverOptions =>
            {
              serverOptions.AddServerHeader = false;
            })
            .UseStartup<Startup>()
            .UseSerilog();
        });
  }
}