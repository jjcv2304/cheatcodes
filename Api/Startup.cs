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
using Persistance;
using Persistance.Utils;
using Presentation.Utils;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var con = new DatabaseSetting(Configuration.GetConnectionString("CheatCodesDatabase"));
            services.AddSingleton(con);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddSingleton<Messages>();
            services.AddHandlers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}