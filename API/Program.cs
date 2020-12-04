using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        //Main is called by dotnet run. 
        public static async Task Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();

            /*
            We are outside of the services container in startup class so
            we do not have access to the startup DbContext instance. ASP.net
            core is not managing lifetime of the DbContext so we must use the
            USING keyword. 

            USING creates a disposable object such that after the code
            in parenthesis is executed, the object is disposed of. 

            */ 
            using (var scope = host.Services.CreateScope()){
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try {
                    var context = services.GetRequiredService<StoreContext>();

                    //applies any pending migrations to the database and creates it
                    //if it doesn't exist
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                } catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during Migration");
                }
            };

            host.Run();
        }

        //the dotnet sdk is using kestrel server as the host, which reads configuration
        //data from appsettings.json. Additional configuration provided in Startup class
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
