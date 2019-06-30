using DocStore.Api.App_Config;
using DocStore.Contract.Manager;
using DocStore.Contract.Repositories;
using DocStore.Domain.Healper;
using DocStore.Domain.Manager;
using DocStore.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
            // Configure and add identity server services
            ConfigurationBuilder.Load(environment);

            services.AddIdentityServer()
                    .AddInMemoryIdentityResources(ConfigurationBuilder.GetIdentityResources())
                    .AddInMemoryApiResources(ConfigurationBuilder.GetApiResources())
                    .AddInMemoryClients(ConfigurationBuilder.GetClients())
                    .AddCustomUserStore()
                    .AddDeveloperSigningCredential();

            services.ConfigureSwagger();

            services.AddTransient<DatabaseHelper>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            
            // Add MVC services
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            //if (environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

            // Use identity server services
            app.UseIdentityServer();

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Equitrix Identity Server API - v1");
                c.RoutePrefix = string.Empty;
            });

            // Use MVC services
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}