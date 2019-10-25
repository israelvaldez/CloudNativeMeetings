using System;
using System.Linq;
using System.Threading.Tasks;
using HealthChecksDemo.Configurations;
using HealthChecksDemo.Extensions;
using HealthChecksDemo.HealthCheckPublishers;
using HealthChecksDemo.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthChecksDemo
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
            services.AddControllers();
            services.AddOptions();
            services.AddSingleton<MemoryHealthCheck>();
            services.Configure<Database>(Configuration.GetSection("Database"));
            var serviceProvider = services.BuildServiceProvider(); //This is a hack
            var databaseSettingsService = serviceProvider.GetRequiredService<IOptions<Database>>();
            services.AddHealthChecks()
                .AddCheck<ExampleHealthCheck>("example_health_check", tags: new string[] { "customer-visible" }) //This is a probe
                .AddSqlServer(databaseSettingsService.Value.SqlConnectionString, tags: new string[] { "customer-invisible" })
                .AddMemoryHealthCheck("memory", failureStatus: HealthStatus.Unhealthy, thresholdInBytes: 3 * 1024L * 1024L, tags: new string[] { "customer-invisible" }); //This is another probe

            services.Configure<HealthCheckPublisherOptions>(options => //this is another way to add something to the configuration pipeline
            {
                options.Delay = TimeSpan.FromSeconds(30);
            });

            services.AddSingleton<IHealthCheckPublisher, ReadinessPublisher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHealthChecks("/health-simple", new HealthCheckOptions()
                {
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
                endpoints.MapHealthChecks("/health-detailed", new HealthCheckOptions()
                {
                    ResponseWriter = WriteResponse,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
                endpoints.MapHealthChecks("/health-customer-visible", new HealthCheckOptions()
                {
                    ResponseWriter = WriteResponse,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
                    Predicate = new Func<HealthCheckRegistration, bool>((registration) =>
                    {
                        if (registration.Tags.Contains("customer-visible"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    })
                });
                endpoints.MapHealthChecks("/health-customer-invisible", new HealthCheckOptions()
                {
                    ResponseWriter = WriteResponse,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
                    Predicate = new Func<HealthCheckRegistration, bool>((registration) =>
                    {
                        if (registration.Tags.Contains("customer-invisible"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    })
                });
                endpoints.MapHealthChecks("/health-html", new HealthCheckOptions()
                {
                    ResponseWriter = WriteHtmlResponse,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
            });
        }

        private static Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data", new JObject(pair.Value.Data.Select(
                            p => new JProperty(p.Key, p.Value))))))))));
            return httpContext.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }

        private static Task WriteHtmlResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "text/html";
            return httpContext.Response.WriteAsync(@"
                    <html>
                        <body>
                            <ul>
                                "
                    +
                    string.Join(Environment.NewLine, result.Entries.Select(pair =>
                        "<li><b>" + pair.Key + " is " + pair.Value.Status.ToString() + "</b></li>"
                        ))
                    +
                    @"
                            </ul>
                        </body>
                    </html>
                    ");
        }
    }
}
