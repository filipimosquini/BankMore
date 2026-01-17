using Microsoft.Extensions.DependencyInjection;

namespace Transfer.Api.Configurations.Extensions;

public static class CorsSetup
{
    public static IServiceCollection AddingCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy
            (
                construtor =>
                {
                    construtor
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }
            );
        });
    }
}