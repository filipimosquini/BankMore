using Account.Infrastructure.Sections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Account.Api.Configuration.Extensions;

public static class AuthenticationSetup
{
    public static IServiceCollection AddingAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection("Identity");
        services.Configure<Identity>(appSettingsSection);

        var identitySettings = appSettingsSection.Get<Identity>();

        services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                
                options.Authority = identitySettings.Authority;
                options.Audience = identitySettings.ValidOn;
               
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RoleClaimType = "role"
                };
            });
        return services;
    }
}