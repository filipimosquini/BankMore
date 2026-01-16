using Authentication.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using Authentication.Api.Documentation.Swagger.Examples;

namespace Authentication.Api.Configurations.Extensions;

public static class SwaggerSetup
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerExamplesFromAssemblyOf<CreateUserCommandExample>();
        services.AddSwaggerGen(options =>
        {
            options.ExampleFilters();

            var xmlFilesPath = "./Documentation";
            var folder = new DirectoryInfo(xmlFilesPath);

            var files = folder.GetFiles("*.xml");

            foreach (var file in files)
            {
                options.IncludeXmlComments(file.FullName, true);
            }

            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Authentication API",
                Description = "API that provides endpoints for authentication services.",
                Contact = new OpenApiContact() { Name = "Filipi Mosquini", Email = "mosquinilabs@gmail.com" },
                License = new OpenApiLicense()
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder builder)
    {
        return builder
            .UseSwagger()
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API"));
    }
}