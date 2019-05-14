using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aggregator.ServiceClients.Api;
using Aggregator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Aggregator
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Aggreator Service",
                    Version = "v1",
                    Description = "Aggregator Service ASP.Net Core 2.2.0 API",
                    TermsOfService = new Uri("https://qrolling.net/termsofservices"),
                    Contact = new OpenApiContact
                    {
                        Name = "Quy Nguyen",
                        Email = "nguyenledinhquy@gmail.com",
                        Url = new Uri("https://twitter.com/nguyenledinhquy")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LIX",
                        Url = new Uri("https://qrolling.net/license")
                    }
                });
            });
            services.AddSingleton<IStore> (new Store());
            services.AddSingleton<ITemperatureHistorian>(new TemperatureHistorian());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            log.AddConsole(Configuration.GetSection("Logging"));
            log.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();
  
            app.UseSwagger(
                c =>
                {
                    //c.RouteTemplate = "api-docs/{documentName}/swagger.json"; 
                    
                }
            );
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs"); });
            app.UseHttpsRedirection();
          
        } 
    }
}