using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyProject
{
    public class Startup
    {


        IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            //======================================================================================================================

            // Video 11 Example 4

            app.Use(async (context, next) =>
            {
                logger.LogInformation("mdl 1 started");
                await next();
                logger.LogInformation("mdl 1 finshed");
            });

            app.Use(async (context, next) =>
            {
                logger.LogInformation("mdl 2 started");
                await next();
                logger.LogInformation("mdl 1 finshed");

            });

            app.Run(async (context) =>
            {
                logger.LogInformation("mdl run started");
                await context.Response.WriteAsync("Hello From Second Mdl\n");
                
            });


            ////======================================================================================================================

            //// Video 11 Example 3

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello From First Mdl \n");
            //    await next();
            //});

            //app.UseRouting();

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello From Second Mdl\n");
            //    await next();
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //var server = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //        var connectionString = _configuration.GetConnectionString("MyFirstCon");
            //        var messgae = _configuration["Key1"].ToString();
            //        await context.Response.WriteAsync($"Hello World! {messgae} connection is {connectionString}");
            //    });
            //});

            ////======================================================================================================================

            //// Video 11 Example 2
            //app.UseStaticFiles();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("hello from Run middleware");
            //});


            ////======================================================================================================================

            //// Video 11 Example 1
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //var server = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //        var connectionString = _configuration.GetConnectionString("MyFirstCon");
            //        var messgae = _configuration["Key1"].ToString();
            //        await context.Response.WriteAsync($"Hello World! {messgae} connection is {connectionString}");
            //    });
            //});
        }
    }
}
