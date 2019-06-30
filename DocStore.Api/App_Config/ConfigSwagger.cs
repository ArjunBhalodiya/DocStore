using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DocStore.Api.App_Config
{
    public static class ConfigSwagger
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Swagger Config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Doc Store API",
                    Description = "Doc Store API",
                    Contact = new Contact
                    {
                        Name = "Arjun Bhalodiya",
                        Email = "adbhalodiya@gmail.com"
                    }
                });

                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteActions();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
