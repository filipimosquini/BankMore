using Authentication.Core.UserAggregate;
using Authentication.Infrastructure.Contexts;
using Authentication.Infrastructure.Sections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace Authentication.Api.Configuration.Extensions;

public static class AuthenticationSetup
{
    public static IServiceCollection AddingAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        var appSettingsSection = configuration.GetSection("Identity");
        services.Configure<Identity>(appSettingsSection);
        var identitySettings = appSettingsSection.Get<Identity>();

        var rsa = RSA.Create();
        rsa.ImportFromPem(identitySettings.RsaPrivateKeyPem);

        var rsaKey = new RsaSecurityKey(rsa)
        {
            KeyId = identitySettings.RsaKeyId
        };
        
        var key = Encoding.ASCII.GetBytes(identitySettings.Secret);

        services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(auth =>
            {
                auth.RequireHttpsMetadata = false;
                auth.SaveToken = true;
                auth.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = rsaKey,

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = identitySettings.ValidOn,
                    ValidIssuer = identitySettings.Issuer
                };
            });

        return services;
    }

    public static IEndpointRouteBuilder MapJwksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/.well-known/jwks.json", (IOptions<Identity> opt) =>
            {
                var identity = opt.Value;

                var rsa = RSA.Create();
                rsa.ImportFromPem(identity.RsaPrivateKeyPem);

                var rsaKey = new RsaSecurityKey(rsa)
                {
                    KeyId = identity.RsaKeyId
                };

                var jwk = JsonWebKeyConverter.ConvertFromSecurityKey(rsaKey);
                jwk.Use = "sig";
                jwk.Alg = SecurityAlgorithms.RsaSha256;
                jwk.Kid = identity.RsaKeyId;

                var jwks = new JsonWebKeySet();
                jwks.Keys.Add(jwk);

                return Results.Json(jwks);
            })
            .WithName("JWKS")
            .WithTags("Auth");

        return endpoints;
    }

    public static IEndpointRouteBuilder MapOpenIdConfiguration(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/.well-known/openid-configuration", (IOptions<Identity> opt) =>
            {
                var identity = opt.Value;
                var issuer = identity.Issuer.TrimEnd('/');

                return Results.Json(new
                {
                    issuer,
                    jwks_uri = $"{issuer}/.well-known/jwks.json"
                });
            })
            .WithName("OpenIdConfiguration")
            .WithTags("Auth");

        return endpoints;
    }
}