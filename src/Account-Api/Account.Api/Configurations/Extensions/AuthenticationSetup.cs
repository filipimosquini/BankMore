using Account.Infrastructure.Sections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;


namespace Account.Api.Configurations.Extensions;

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
                    RoleClaimType = "role",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
                            return Task.CompletedTask;

                        var raw = authHeader.ToString();

                        if (string.IsNullOrWhiteSpace(raw))
                            return Task.CompletedTask;

                        if (!raw.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                            return Task.CompletedTask;

                        var token = raw.Length > "Bearer".Length ? raw["Bearer".Length..].Trim() : string.Empty;

                        if (string.IsNullOrWhiteSpace(token))
                        {
                            context.Token = null;
                            return Task.CompletedTask;
                        }

                        context.Token = token;
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        context.HttpContext.RequestServices
                            .GetRequiredService<ILoggerFactory>()
                            .CreateLogger("JwtBearer")
                            .LogError(context.Exception, "JWT authentication failed");

                        context.HttpContext.Items["AuthFailureReason"] =
                            context.Exception is SecurityTokenExpiredException
                                ? "TOKEN_EXPIRED"
                                : "TOKEN_INVALID";

                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        context.Request.Headers.TryGetValue("Authorization", out var authHeader);
                        var raw = authHeader.ToString();

                        var hasBearerToken =
                            !string.IsNullOrWhiteSpace(raw) &&
                            raw.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase) &&
                            !string.IsNullOrWhiteSpace(raw["Bearer".Length..].Trim());

                        if (!hasBearerToken)
                            return Task.CompletedTask;

                        context.HandleResponse();

                        var reason = context.HttpContext.Items["AuthFailureReason"]?.ToString();

                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var body = reason == "TOKEN_EXPIRED"
                            ? new
                            {
                                notifications = new[]
                                {
                                    new
                                    {
                                        code = "Expired.Token",
                                        message = "The token was expired."
                                    }
                                }
                            }
                            : new
                            {
                                notifications = new[]
                                {
                                    new
                                    {
                                        code = "Invalid.Token",
                                        message = "The token is invalid."
                                    }
                                }
                            };

                        return context.Response.WriteAsJsonAsync(body);
                    }
                };
            });
        return services;
    }
}