using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // doing this so seeding occurs before building
            var host = CreateWebHostBuilder(args);

            // every time web server is started we attempt to seed db to make sure all
            // different parts of db required are going to be in there including running migrations
            // only happens on startup
            SeedDb(host);

            host.Run();
        }

        private static void SeedDb(IWebHost host)
        {
            // scopeFactory is used to create scopes for the lifetime of requests
            var scopeFactory = host.Services.GetService <IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {

                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>()
                .Build();

        private static void SetupConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            // removing default configuration options
            builder.Sources.Clear();

            // builder allows to add different kinds of config information
            // here we are making config.json file, optional is false, reload on change is true so program does not have to restart 
            builder.AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }
    }
}
