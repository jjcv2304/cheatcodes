using System;
using System.Collections.Generic;
using Application;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistance;
using Persistance.Utils;
using Presentation.Utils;
using System;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Http;
using Presentation.Logs.Extensions;
using Presentation.Logs.Filters;
using Presentation.Logs.Middleware;

namespace Presentation
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
            services.AddSingleton<IScopeInformation, ScopeInformation>();

            services.AddMvc(options =>
                options.Filters.Add(typeof(TrackActionPerformanceFilter))
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration.GetConnectionString("CheatCodesDatabase");
            var con = new DatabaseSetting(connectionString);
            services.AddSingleton(con);
            var queriesConnectionString = new QueriesConnectionString(connectionString);
            services.AddSingleton(queriesConnectionString);
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}
            app.UseApiExceptionHandler(options => options.AddResponseDetails = UpdateApiErrorResponse);

            app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });


            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
        {
            if (ex.GetType().Name == typeof(SqlException).Name)
            {
                error.Detail = "Exception was a database exception!";
            }

            //error.Links = "https://gethelpformyerror.com/";
        }
    }
}