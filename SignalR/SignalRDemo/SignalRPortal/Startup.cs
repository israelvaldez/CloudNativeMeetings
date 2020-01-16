using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace SignalRPortal
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

            services.AddSignalR()
                // For Azure signalR
                .AddAzureSignalR("Endpoint=https://signalrepicordemo.service.signalr.net;AccessKey=Ac4ybDUEo4JF1+4ncrrF5iI1vxnxcde0Q8WQ3CwwJe4=;Version=1.0;");
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder.SetIsOriginAllowed(r => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });

            app
                //.UseSignalR
                .UseAzureSignalR
                (routes =>
            {
                // the url most start with lower letter
                routes.MapHub<EventHub>("/eventHub");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
