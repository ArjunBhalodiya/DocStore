using System;
using System.Collections.Generic;
using System.IO;
using DocStore.Api.IdentityModels;
using DocStore.Contract.Configurations;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace DocStore.Api.App_Config
{
    public static class ConfigurationBuilder
    {
        private static AppConfigurations AppConfigurations { get; set; }
        private static List<Client> Clients { get; set; }
        private static List<ApiResource> ApiResources { get; set; }
        private static List<IdentityResource> IdentityResources { get; set; }


        public static void Load(IHostingEnvironment environment)
        {
            var idServerConfigPath = Path.Combine(environment.ContentRootPath, $"identityServer.config.{environment.EnvironmentName}.json");
            if (!File.Exists(idServerConfigPath) && environment.IsDevelopment())
            {
                idServerConfigPath = Path.Combine(environment.ContentRootPath, $"identityServer.config.json");
            }

            if (!File.Exists(idServerConfigPath))
            {
                throw new FileNotFoundException($"IdentityServer configuration file not found at: {idServerConfigPath}");
            }

            var appConfigPath = Path.Combine(environment.ContentRootPath, $"appsettings.{environment.EnvironmentName}.json");
            if (!File.Exists(appConfigPath) && environment.IsDevelopment())
            {
                appConfigPath = Path.Combine(environment.ContentRootPath, $"appsettings.json");
            }

            if (!File.Exists(appConfigPath))
            {
                throw new FileNotFoundException($"IdentityServer settings file not found at: {appConfigPath}");
            }

            var idServerConfigJson = File.ReadAllText(idServerConfigPath);
            var idServerConfig = JsonConvert.DeserializeObject<ConfigurationModel>(idServerConfigJson);
            SetClients(idServerConfig);
            SetApiResources(idServerConfig);
            SetIdentityResources(idServerConfig);

            var appConfigJson = File.ReadAllText(appConfigPath);
            var appConfig = JsonConvert.DeserializeObject<AppConfigurations>(appConfigJson);
            SetAppConfigurations(appConfig);
        }

        public static IEnumerable<Client> GetClients()
        {
            return Clients;
        }
        private static void SetClients(ConfigurationModel config)
        {
            Clients = new List<Client>();

            foreach (var client in config.Clients)
            {
                Clients.Add(new Client
                {
                    ClientId = client.ClientId,
                    ClientSecrets = { new Secret(client.ClientSecrets.Sha256()) },

                    AllowedScopes = client.AllowedScopes ?? new List<string>(),
                    AllowedGrantTypes = client.GetClientType(),

                    RedirectUris = client.RedirectUris ?? new List<string>(),
                    PostLogoutRedirectUris = client.PostLogoutRedirectUris ?? new List<string>(),
                    AllowedCorsOrigins = client.AllowedCorsOrigins ?? new List<string>(),

                    AllowOfflineAccess = client.AllowOfflineAccess,
                    RequirePkce = client.RequirePkce,
                    RequireClientSecret = client.RequireClientSecret,
                    AllowRememberConsent = client.AllowRememberConsent,
                    RequireConsent = client.RequireConsent,

                    AllowAccessTokensViaBrowser = true,
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt
                });
            }
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return ApiResources;
        }
        private static void SetApiResources(ConfigurationModel config)
        {
            ApiResources = new List<ApiResource>();

            foreach (var apiResource in config.ApiResources)
            {
                ApiResources.Add(new ApiResource(apiResource.Name, apiResource.DisplayName, apiResource.ClaimTypes ?? null));
            }
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return IdentityResources;
        }
        private static void SetIdentityResources(ConfigurationModel config)
        {
            IdentityResources = new List<IdentityResource>();

            foreach (var identityResource in config.IdentityResources)
            {
                if (!Enum.TryParse(identityResource, out IdentityResourcesType identityResourcesType))
                {
                    throw new InvalidDataException($"Invalid Identity Resources Type in IdentityServer configuration.");
                }

                switch (identityResourcesType)
                {
                    case IdentityResourcesType.Address:
                        IdentityResources.Add(new IdentityResources.Address());
                        break;
                    case IdentityResourcesType.Email:
                        IdentityResources.Add(new IdentityResources.Email());
                        break;
                    case IdentityResourcesType.OpenId:
                        IdentityResources.Add(new IdentityResources.OpenId());
                        break;
                    case IdentityResourcesType.Phone:
                        IdentityResources.Add(new IdentityResources.Phone());
                        break;
                    case IdentityResourcesType.Profile:
                        IdentityResources.Add(new IdentityResources.Profile());
                        break;
                    default:
                        throw new InvalidDataException($"Invalid identity resources type {identityResourcesType}");
                }
            }
        }

        public static AppConfigurations GetAppConfigurations()
        {
            return AppConfigurations;
        }
        private static void SetAppConfigurations(AppConfigurations config)
        {
            AppConfigurations = config;
        }
    }
}