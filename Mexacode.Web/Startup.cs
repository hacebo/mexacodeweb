using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mexacode.Web.Services;
using Serilog;
using Serilog.Events;
using System.IO;
using Mexacode.Web.Models;

namespace Mexacode.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //var logPath = @"C:\\inetpub\\logs\\appLogs\\mexacode\\web\\";
            var logPath = "wwwroot";
            if (env.IsDevelopment() || env.IsEnvironment("Debug"))
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.RollingFile(Path.Combine(logPath, "log-{Date}.txt"), LogEventLevel.Debug)
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Warning()
                    .WriteTo.RollingFile(Path.Combine(logPath, "log-{Date}.txt"), LogEventLevel.Warning)
                    .CreateLogger();
            }

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.Configure<AppSettings>(appSettings =>
            {
                appSettings.ContactUs = new ContactUs()
                {
                    // Untyped Syntax - Configuration[""]
                    Email = Configuration["ContactUs:Email"],
                    Password = Configuration["ContactUs:Password"]
                };
            });
            services.AddScoped<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
