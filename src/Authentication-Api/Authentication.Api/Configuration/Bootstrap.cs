using Authentication.Api.Configurations.Validators;
using Authentication.Application.Users.Commands.CreateUser;
using Authentication.Infrastructure.CrossCutting.ResourcesCatalog;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Authentication.Application.Services;

namespace Authentication.Api.Configuration;

public static class Bootstrap
{
    public static IServiceCollection AddResourcesDependencies(this IServiceCollection services)
    {
        return services.AddSingleton<IResourceCatalog, ResourceCatalog>();
    }

    public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
    {
        return services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    public static IServiceCollection AddValidatorDependencies(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
    }

    public static IServiceCollection AddMediatrDependencies(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(CreateUserCommandHandler)), // Application
        };

        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    }
}