using System;
using System.IO;
using Account.Api.Documentation.Swagger.Examples;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Account.Api.Configuration.Extensions;

public static class SwaggerSetup
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerExamplesFromAssemblyOf<DefaultRequestExample>();
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

            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Account API",
                Description = "API that provides endpoints for account services.",
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
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account API"));
    }
}