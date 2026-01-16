using Authentication.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure.Configurations;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("MySqlConnection");

        services.AddDbContext<IdentityContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection),
                x => x.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

        return services;
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var identityContext = serviceScope.ServiceProvider.GetService<IdentityContext>();

        if (identityContext.MigrateDatabase()) return builder;

        if (!identityContext.MigrateDatabase())
        {
            identityContext.Database.Migrate();
        }

        return builder;
    }
}