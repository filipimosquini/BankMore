using Account.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure.Configurations;

public static class DatabaseConfiguration 
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var oracleConnection = configuration.GetConnectionString("OracleConnection");

        services.AddDbContext<DatabaseContext>(options =>
            options.UseOracle(oracleConnection, oracleOptions => oracleOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

        return services;
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();

        if (context.MigrateDatabase()) return builder;

        if (!context.MigrateDatabase())
        {
            context.Database.Migrate();
        }

        return builder;
    }
}