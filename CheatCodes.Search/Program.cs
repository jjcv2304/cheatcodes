using System;
using System.IO;
using CheatCodes.Search.RabbitMQ;
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
        InitializeRabbitMq(host);
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


    private static void InitializeRabbitMq(IHost host)
    {
      var serviceScope = host.Services.CreateScope();
      var services = serviceScope.ServiceProvider;
      var client = new RabbitMQConsumer(services);
      client.CreateConnection();
      client.ProcessMessages();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
        .UseSerilog();

    }

  }
}