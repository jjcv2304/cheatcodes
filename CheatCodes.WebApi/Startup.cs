﻿using Application.Categories.Queries;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

namespace CheatCodes.WebApi
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
            services.AddDbContext<DatabaseService>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CheatCodesDatabase")));
//            optionsBuilder
//                .UseSqlServer(connectionString, providerOptions=>providerOptions.CommandTimeout(60))
//                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            services.AddScoped<IGetCategories, GetCategories>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}