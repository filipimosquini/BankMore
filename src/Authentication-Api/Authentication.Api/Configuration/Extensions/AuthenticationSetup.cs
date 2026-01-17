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

        // Cache RSA key at once using singleton
        services.AddSingleton<RsaSecurityKey>(sp =>
        {
            var identity = sp.GetRequiredService<IOptions<Identity>>().Value;

            var rsa = RSA.Create();
            rsa.ImportFromPem(identity.RsaPrivateKeyPem);

            return new RsaSecurityKey(rsa)
            {
                KeyId = identity.RsaKeyId
            };
        });

        // Cache JWKS at once using singleton
        services.AddSingleton<JsonWebKeySet>(sp =>
        {
            var identity = sp.GetRequiredService<IOptions<Identity>>().Value;
            var rsaKey = sp.GetRequiredService<RsaSecurityKey>();

            var jwk = JsonWebKeyConverter.ConvertFromSecurityKey(rsaKey);
            jwk.Use = "sig";
            jwk.Alg = SecurityAlgorithms.RsaSha256;
            jwk.Kid = identity.RsaKeyId;

            var jwks = new JsonWebKeySet();
            jwks.Keys.Add(jwk);

            return jwks;
        });


        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();

        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<Identity>, RsaSecurityKey>((options, identityOpt, rsaKey) =>
            {
                var identity = identityOpt.Value;

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = rsaKey,

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = identity.ValidOn,
                    ValidIssuer = identity.Issuer
                };
            });

        return services;
    }

    public static IEndpointRouteBuilder MapJwksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        // JWKS returned from singleton (RSA is not Created by request)
        endpoints.MapGet("/.well-known/jwks.json", (JsonWebKeySet jwks) => Results.Json(jwks))
            .WithName("JWKS")
            .WithTags("JWKS");

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
            .WithTags("JWKS");

        return endpoints;
    }
}