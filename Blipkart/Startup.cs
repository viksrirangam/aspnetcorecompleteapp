using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Blipkart.Model;
using Blipkart.ViewModel;
using Blipkart.Repository;
using Blipkart.Service;
using Blipkart.Core.Security;

namespace Blipkart
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ShoppingContext>(options => options.UseSqlServer(connection));
            //services.AddDbContext<BloggingContext>(options => options.UseInMemoryDatabase());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IIdentityHelper, IdentityHelper>();

            services.AddScoped<DbContext, ShoppingContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<ICartService, CartService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                  options.CookieName = Configuration["Session:CookieName"];
                  options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .WithFilter(new FilterLoggerSettings
                    {
                        { "Microsoft", LogLevel.Warning },
                        { "System", LogLevel.Warning },
                        { "Blipkart", LogLevel.Debug }
                    })
                .AddConsole();

            // add Trace Source logging
            var testSwitch = new SourceSwitch("sourceSwitch", "Logging");
            testSwitch.Level = SourceLevels.Error;
            //loggerFactory.AddTraceSource(testSwitch, new EventLogTraceListener("Blipkart"));
            loggerFactory.AddTraceSource(testSwitch, new TextWriterTraceListener(writer: Console.Out));

            app.UseSession();

            app.Use(async (context, next) =>
            {
                if (context.Session.GetString("__id__") == null)
                {
                    context.Session.SetString("__id__", "1221");
                }

                await next.Invoke();
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "BlipkartCookieMiddlewareInstance",
                LoginPath = new PathString("/Account/Login"),
                AccessDeniedPath = new PathString("/Account/Forbidden"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true/*,
                Events = new CookieAuthenticationEvents
                {
                    // Set other options
                    OnRedirectToAccessDenied = OnAccessDenied.RedirectOnAccessDenied
                }*/
            });

            app.UseStaticFiles();

            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMvc(routes =>
			{
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

/*
expire cached cart when session id changes.
ef logging, model logging(via action filter)
tests for repository, views
*/
