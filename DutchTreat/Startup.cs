using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // makes DbContext part of services so it can be used for example inside a controller
            services.AddDbContext<DutchContext>(cfg =>
            {
                // configure this context to use SqlServer
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });

            services.AddAutoMapper();

            services.AddTransient<DutchSeeder>();

            services.AddTransient<IMailService, NullMailService>();
            // Support for real mail service

            // want respoitory in one scope, we are saying to use IDutchRepository interface, and the implementation DutchRepository
            services.AddScoped<IDutchRepository, DutchRepository>();

            services.AddMvc()
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // when not in development mode show this page for exceptions
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseNodeModules(env);

            app.UseMvc(config =>
            {
                config.MapRoute("Default", 
                    "/{controller}/{action}/{id?}", 
                    new { controller = "App", Action = "Index" });
            });
        }
    }
}
