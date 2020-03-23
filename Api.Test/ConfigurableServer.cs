using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Test
{
  public class ConfigurableServer : TestServer
  {
    public ConfigurableServer(Action<IServiceCollection> configureAction = null) : base(CreateBuilder(configureAction))
    {
    }

    private static IWebHostBuilder CreateBuilder(Action<IServiceCollection> configureAction)
    {
      if (configureAction == null) configureAction = sc => { };
      var builder = new WebHostBuilder()
        .ConfigureServices(sc => sc.AddSingleton(configureAction))
        .UseStartup<ConfigurableStartup>();
      return builder;
    }
  }
}