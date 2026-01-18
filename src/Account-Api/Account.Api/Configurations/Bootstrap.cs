using Account.Api.Configurations.Validators;
using Account.Application.Account.Commands.CreateAccount;
using Account.Application.Services;
using Account.Core.AccountAggregate.Repositories;
using Account.Core.MovementAggregate.Repositories;
using Account.Infrastructure.CrossCutting.ResourcesCatalog;
using Account.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Account.Api.Configurations;

public static class Bootstrap
{
    public static IServiceCollection AddResourcesDependencies(this IServiceCollection services)
    {
        return services
            .AddSingleton<IResourceCatalog, ResourceCatalog>();
    }

    public static IServiceCollection AddRepositoriesDependencies(this IServiceCollection services)
    {
        return services
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IMovementRepository, MovementRepository>();
    }

    public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
    {
        return services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IMovementService, MovementService>();
    }

    public static IServiceCollection AddValidatorDependencies(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssemblyContaining<CreateAccountCommand>();
    }

    public static IServiceCollection AddMediatrDependencies(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(CreateAccountCommandHandler)), // Application
        };

        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    }
}