using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alexa.School.Web
{
    public class Startup
    {
        #region Constructors

        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(basePath: env.ContentRootPath)
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        #endregion

        #region Properties, Indexers

        public IConfigurationRoot Configuration { get; }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(configuration: this.Configuration.GetSection(key: "Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }

        #endregion
    }
}