// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.Configuration;
using Microsoft.Extensions.Configuration;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IdentityServer
{
  public class Startup
  {
    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment environment, IConfiguration configuration)
    {
      Environment = environment;
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

      string connectionString = Configuration.GetConnectionString("DefaultConnection");

      var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

      services.AddTransient<IEmailSender, EmailSender>();
      services.Configure<AuthMessageSenderOptions>(Configuration);

      services.AddControllersWithViews();
      services.AddRazorPages();

      services.AddDbContext<IdentityDbContext>(options =>
        options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
      );
      services.AddDbContext<Data.ConfigurationDbContext>(options =>
        options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
        );

      services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
          options.SignIn.RequireConfirmedEmail = true;
          options.SignIn.RequireConfirmedAccount = true;
        })
        .AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders();

      var builder = services.AddIdentityServer(options =>
        {
          options.Events.RaiseErrorEvents = true;
          options.Events.RaiseInformationEvents = true;
          options.Events.RaiseFailureEvents = true;
          options.Events.RaiseSuccessEvents = true;
          //options.UserInteraction.LoginUrl = "/Account/Login";
          //options.UserInteraction.LogoutUrl = "/Account/Logout";
          options.UserInteraction.LoginUrl = "/Identity/Account/Login";
          options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
          options.Authentication = new AuthenticationOptions()
          {
            CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
            CookieSlidingExpiration = true
          };
        })
        .AddConfigurationStore(options =>
        {
          options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
        })
        .AddOperationalStore(options =>
        {
          options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
          options.EnableTokenCleanup = true;
        })
        .AddAspNetIdentity<ApplicationUser>();

      // not recommended for production - you need to store your key material somewhere secure
      builder.AddDeveloperSigningCredential();
    }

    public void Configure(IApplicationBuilder app)
    {
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // uncomment if you want to add MVC
      app.UseStaticFiles();
      app.UseRouting();

      app.UseIdentityServer();

      // uncomment, if you want to add MVC
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
      });
    }
  }
}
