using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheatCodes.WebApi.Models;
using CheatCodes.WebApi.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.AddDbContext<CheatCodesDb>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CheatCodesDatabase")));
//            optionsBuilder
//                .UseSqlServer(connectionString, providerOptions=>providerOptions.CommandTimeout(60))
//                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            services.AddTransient<ICategoryService, CategoryService>();
            
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