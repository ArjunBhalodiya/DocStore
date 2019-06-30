using DocStore.Api.IdentityServices;
using DocStore.Api.IdentityValidators;
using Microsoft.Extensions.DependencyInjection;

namespace DocStore.Api.App_Config
{
    public static class ConfigurationBuilderExtensions
    {
        /*Follow below links to implement non implemented methods
        https://damienbod.com/2017/04/14/asp-net-core-identityserver4-resource-owner-password-flow-with-custom-userrepository
        and https://github.com/damienbod/AspNetCoreIdentityServer4ResourceOwnerPassword */

        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.AddProfileService<IdentityProfileService>();
            builder.AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>();
            return builder;
        }
    }
}