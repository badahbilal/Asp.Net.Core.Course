using EmptyProject.Models;
using EmptyProject.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            //Cette ligne pour injecter Notre Db context à l'application en utilisant
            //le parametre de connection à partir le ficher appsettings.json
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("EmployeeDbConnection")));


            //cette ligne permet d'ajouter les services d'identification à note application
            // et on met une liaison le service identity et dbcontext de l'application 
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //Les lignes suivants nous permetent de gérer la complexité de mods de passe 

                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<AppDbContext>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 3;
            //});



            // Cette ligne vous permez d'ajouter les services de MVC
            services.AddMvc(options =>
                        {
                            options.EnableEndpointRouting = false;

                            //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                            //options.Filters.Add(new AuthorizeFilter(policy));

                        }
            );

            //ce ligne change le path par defaut de connection à l'application de Account/login vers d'autre path 
            //services.ConfigureApplicationCookie(options => options.LoginPath = "/test1/test2");




            // cette ligne vous permet d'injecter le repo pour le controlleur (dependency injection)
            services.AddSingleton<IAnimleReposiroty<Cat>, CatRepository>();
            //services.AddScoped<ICompanyRepository<Employee>, Employee2Repository>();
            //services.AddSingleton<ICompanyRepository<Employee>, EmployeeRepository>();
            //services.AddScoped<ICompanyRepository<Employee>, EmployeeRepository>();
            //services.AddTransient<ICompanyRepository<Employee>, EmployeeRepository>();

            // l'injection du SQLEmployeeRepository pour manipuler les données avec la base de données sql server
            services.AddScoped<ICompanyRepository<Employee>, SQLEmployeeRepository>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseFileServer();

            // on ajoute le middleware de l'authentification
            app.UseAuthentication();

            // ce ligne pour ajouter le middleware qui implimente le mvc middleware.
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaults", "{controller=Employee2}/{action=index}/{id?}");
            });

            //app.Run(async (context) =>
            //{

            //    await context.Response.WriteAsync("hello world !");
            //});



        }
    }
}


////======================================================================================================================

//// Video 16

//if (env.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

//app.UseFileServer();

//// ce ligne pour ajouter le middleware qui implimente le mvc middleware.
//app.UseMvcWithDefaultRoute();


////app.UseMvc(routes =>
////{
////    routes.MapRoute("defaults", "{controller=Product}/{action=MyAction}/{id?}");
////});


//app.Run(async (context) =>
//{

//    await context.Response.WriteAsync("hello world !");
//});



////======================================================================================================================

//// Video 11 Example 4

//app.Use(async (context, next) =>
//{
//    logger.LogInformation("mdl 1 started");
//    await next();
//    logger.LogInformation("mdl 1 finshed");
//});

//app.Use(async (context, next) =>
//{
//    logger.LogInformation("mdl 2 started");
//    await next();
//    logger.LogInformation("mdl 1 finshed");

//});

//app.Run(async (context) =>
//{
//    logger.LogInformation("mdl run started");
//    await context.Response.WriteAsync("Hello From Second Mdl\n");

//});


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