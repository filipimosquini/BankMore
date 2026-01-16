using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Authentication.Infrastructure.Contexts;
using Authentication.Infrastructure.Sections;

namespace Authentication.Api.Configurations.Extensions;

public static class AuthenticationSetup
{
    public static IServiceCollection AddingAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        var appSettingsSection = configuration.GetSection("Identity");
        services.Configure<Identity>(appSettingsSection);

        var identitySettings = appSettingsSection.Get<Identity>();
        var key = Encoding.ASCII.GetBytes(identitySettings.Secret);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(auth =>
        {
            auth.RequireHttpsMetadata = false;
            auth.SaveToken = true;
            auth.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = identitySettings.ValidOn,
                ValidIssuer = identitySettings.Issuer
            };
        });

        return services;
    }
}