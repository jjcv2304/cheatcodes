using System;
using System.IO;
using System.Threading.Tasks;
using CheatCodes.Search.RabbitMQ;
using CheatCodes.Search.RabbitMQ.Handlers;
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

    public static async Task Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .WriteTo.Console()
        .WriteTo.Debug()
        .WriteTo.File(new JsonFormatter(), @"c:\temp\logs\cheatcodes_search_api_log.json", shared: true)
        .CreateLogger();

      var host = CreateHostBuilder(args).Build();

      try
      {
        Log.Information("Starting web host");

        InitializeRabbitMq(host);
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
      }
      finally
      {
        Log.CloseAndFlush();
      }

      await host.RunAsync();
    }

    private static void InitializeRabbitMq(IHost host)
    {
      var serviceScope = host.Services.CreateScope();
      var services = serviceScope.ServiceProvider;
      var newCategoryEventHandler = services.GetRequiredService<INewCategoryEventHandler>();
      var client = new RabbitMQConsumer(newCategoryEventHandler);
      client.CreateConnection();
      client.ProcessMessages();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).UseSerilog();
    }
  }
}