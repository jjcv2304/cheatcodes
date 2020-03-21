using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.RabbitMQ;
using CheatCodes.Search.RabbitMQ.Handlers;
using CheatCodes.Search.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.OpenApi.Models;
using ICategoriesChangesRepository = CheatCodes.Search.Repositories.ICategoriesChangesRepository;

namespace CheatCodes.Search
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public static readonly ILoggerFactory MyLoggerFactory
      = LoggerFactory.Create(builder => { builder.AddEventLog(); });

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<ICategoriesSearchRepository, CategoriesSearchRepository>();
      services.AddTransient<ICategoriesChangesRepository, CategoriesChangesRepository>();
      services.AddTransient<INewCategoryEventHandler, NewCategoryEventHandler>();

      var connectionString = Configuration.GetConnectionString("CheatCodesDatabase");
      services.AddDbContext<CheatCodesDbContext>(options =>
        options.UseLoggerFactory(MyLoggerFactory)
        .UseSqlite(connectionString));

      //services.AddDbContext<CheatCodesDbContext2>(options =>
      //  options.UseLoggerFactory(MyLoggerFactory)
      //    .UseSqlite(connectionString));

      services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

      services.AddControllers();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "CheatCodes Search API",
          Description = "Api for the search screen",
          TermsOfService = new Uri("https://example.com/terms"),
          Contact = new OpenApiContact
          {
            Name = "Juan",
            Email = "jj@fakemail.com",
          },
          License = new OpenApiLicense
          {
            Name = "Use under ...",
            Url = new Uri("https://example.com/license"),
          }
        });
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseSwagger();
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
