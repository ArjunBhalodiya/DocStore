using DocStore.Api.App_Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DocStore.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment environment;

        public Startup(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Load configurations
            ConfigurationBuilder.Load(environment);

            // Configure identity server services
            services.AddIdentityServer()
                    .AddInMemoryIdentityResources(ConfigurationBuilder.GetIdentityResources())
                    .AddInMemoryApiResources(ConfigurationBuilder.GetApiResources())
                    .AddInMemoryClients(ConfigurationBuilder.GetClients())
                    .AddCustomUserStore()
                    .AddDeveloperSigningCredential();

            // Configure swagger
            services.ConfigureSwagger();

            // Configure system level dependencies
            services.AddDependencies();

            // Add MVC services
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use identity server services
            app.UseIdentityServer();

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocStore API - v1");
                c.RoutePrefix = string.Empty;
            });

            // Use sirilog
            loggerFactory.AddSerilog();

            // Use MVC services
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}