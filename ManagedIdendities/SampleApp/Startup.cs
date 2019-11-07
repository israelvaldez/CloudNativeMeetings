using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SampleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)

        {

            Configuration = configuration;

        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

                var document = string.Format(Markup.Text, Configuration["SecretName"], Configuration["Section:SecretName"], Configuration.GetSection("Section")["SecretName"]);

                context.Response.ContentLength = encoding.GetByteCount(document);

                context.Response.ContentType = "text/html";

                await context.Response.WriteAsync(document);

            });
        }
    }
}
