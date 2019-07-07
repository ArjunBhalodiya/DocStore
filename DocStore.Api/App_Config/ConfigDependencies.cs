using DocStore.Contract.Manager;
using DocStore.Contract.Repositories;
using DocStore.Domain.Helper;
using DocStore.Domain.Manager;
using DocStore.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DocStore.Api.App_Config
{
    public static class ConfigDependencies
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient(s => new DatabaseHelper(ConfigurationBuilder.GetAppConfigurations()
                                                                              .ConnectionStrings.DefaultConnection,
                                                          s.GetService<ILogger<DatabaseHelper>>()));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            return services;
        }
    }
}