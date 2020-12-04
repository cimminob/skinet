using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    //when application starts it creates instance of Startup
    // with configuration from appsettings.json injected
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        //Dependency Injection Container
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* 
            AddScoped refers to creating a contextLifetime of Scoped, which means the service
            will be availabe for entire lifetime of the http request
            
            AddTransient - a transient service is instantiated for an invididual method rather
            than the request itself, so its lifetime is very short.

            AddSingleton - a singleton service is created when a request is made, but is never
            destroyed until the application shuts down.
            */
            
            services.AddScoped<IProductRepository, ProductRepository>();

            //Generic Repository service
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
        }

        //Middleware is added here
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //serve up static content such as images - must come before Routing!!!
            app.UseStaticFiles();

            //ASP.NET Core controllers use the Routing middleware to match the URLs of 
            //incoming requests and map them to actions
            app.UseRouting();

            app.UseAuthorization();

            /* 
            Endpoints are the app's units of executable request-handling code. Endpoints 
            are defined in the app and configured when the app starts. The endpoint matching 
            process can extract values from the request's URL and provide those values for 
            request processing.
            */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
