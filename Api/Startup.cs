﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Api.Logs.Extensions;
using Api.Logs.Filters;
using Api.Logs.Middleware;
using Api.Security;
using Api.Utils;
using Application.Utils;
using Application.Utils.Interfaces;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Persistance;
using Persistance.Utils;

namespace Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options =>
        {
          var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
          options.EnableEndpointRouting = false;
          options.Filters.Add(new AuthorizeFilter(policy));
          options.Filters.Add(typeof(TrackActionPerformanceFilter));
        }
      ).SetCompatibilityVersion(CompatibilityVersion.Latest);

      //services.AddNewtonsoftJson();

      services.AddSingleton<IScopeInformation, ScopeInformation>();

      services.AddCors(o =>
      {
        o.AddPolicy("ApiCorsPolicy", builder =>
        {
          builder.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => origin == "http://localhost:4200")
            .AllowCredentials();
        });
      });

      services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
        .AddIdentityServerAuthentication(options =>
        {
          //options.Authority = "http://localhost:5000";
          options.Authority = "https://localhost:5002";
          options.ApiName = "mainApp-api";
          options.RequireHttpsMetadata = false;
        });

      var connectionString = Configuration.GetConnectionString("CheatCodesDatabase");
      var con = new DatabaseSetting(connectionString);
      services.AddSingleton(con);
      var queriesConnectionString = new QueriesConnectionString(connectionString);
      services.AddSingleton(queriesConnectionString);
     // services.AddTransient<IDbTransaction, DbTransaction>();
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddTransient<ICategoryQueryRepository, CategoryQueryRepository>();
      services.AddTransient<ICategoryCommandRepository, CategoryCommandRepository>();
       

      services.AddSingleton<Messages>();


      services.AddHandlers();

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "CheatCodes API",
          Description = "A simple example ASP.NET Core Web API",
          TermsOfService = new Uri("https://example.com/terms"),
          Contact = new OpenApiContact
          {
            Name = "Juan",
            Email = "jj@fakemail.com"
          },
          License = new OpenApiLicense
          {
            Name = "Use under ...",
            Url = new Uri("https://example.com/license")
          }
        });
          // Set the comments path for the Swagger JSON and UI.
          var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
      services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
      ConfigureAdditionalServices(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseCors("ApiCorsPolicy");

      app.UseSecurityHeaders();
      app.UseStaticFiles();
      app.UseApiExceptionHandler(options => options.AddResponseDetails = UpdateApiErrorResponse);

      app.UseSwagger();
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

      app.UseHsts();
      app.UseHttpsRedirection();
      app.UseAuthentication();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });

      app.UseRouting();

      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
      //  {
      //    ResultStatusCodes = {
      //      [HealthStatus.Healthy] = StatusCodes.Status200OK,
      //      [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
      //      [HealthStatus.Unhealthy] =StatusCodes.Status503ServiceUnavailable
      //    },
      //    // ResponseWriter = WriteHealthCheckReadyResponse,
      //    Predicate = (check) => check.Tags.Contains("ready"),
      //    AllowCachingResponses = false
      //  });

      //  endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
      //  {
      //    Predicate = (check) => !check.Tags.Contains("ready"),
      //    // ResponseWriter = WriteHealthCheckLiveResponse,
      //    AllowCachingResponses = false
      //  });
      //});
    }

    protected virtual void ConfigureAdditionalServices(IServiceCollection services)
    {
      //services.AddHealthChecks()
      //  .Add(connectionString, failureStatus: HealthStatus.Unhealthy, tags: new[] { "ready" })
      //  .AddUrlGroup(new Uri($"{stockIndexServiceUrl}/api/StockIndexes"),
      //    "Stock Index Api Health Check", HealthStatus.Degraded, tags: new[] { "ready" }, timeout: new TimeSpan(0, 0, 5))
      //  .AddFilePathWrite(securityLogFilePath, HealthStatus.Unhealthy, tags: new[] { "ready" });
    }

    private void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
    {
      if (ex.GetType().Name == nameof(DbException)) error.Detail = "Exception was a database exception!";

      //error.Links = "https://gethelpformyerror.com/";
    }
  }
}