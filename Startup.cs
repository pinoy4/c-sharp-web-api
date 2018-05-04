using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MWTest.ConfigurationOptions;
using MWTest.Extensions;
using MWTest.Middleware;

namespace MWTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add database service
            var dbConnectionOptions = Configuration.GetSection("DBConnectionOptions");
            services.AddMWTestDbService(dbConnectionOptions);

            // Add JWT Authentication
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.AddMWTestJwtServices(jwtAppSettingOptions);

            // Add other application dependencies
            services.AddMWTestServices();

            // Add MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMiddleware<DateTimeHeaderMiddleware>();

            app.UseMvc();
        }
    }
}