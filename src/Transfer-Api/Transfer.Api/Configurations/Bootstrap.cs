using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Transfer.Api.Configurations.Validators;
using Transfer.Application.Common.Idempotencies.Behaviors;
using Transfer.Application.Common.Integrations;
using Transfer.Application.Services;
using Transfer.Application.Transfer.Commands.CreateTransfer;
using Transfer.Core.Common.Indepotencies.Hashing;
using Transfer.Core.Common.Indepotencies.Repositories;
using Transfer.Core.TransferAggregate.Repositories;
using Transfer.Infrastructure.Common.Idempotencies.Hashing;
using Transfer.Infrastructure.Common.Idempotencies.Repositories;
using Transfer.Infrastructure.Common.Integrations.AccountApi;
using Transfer.Infrastructure.CrossCutting.ResourcesCatalog;
using Transfer.Infrastructure.Repositories;

namespace Transfer.Api.Configurations;

public static class Bootstrap
{
    public static IServiceCollection AddResourcesDependencies(this IServiceCollection services)
    {
        return services.AddSingleton<IResourceCatalog, ResourceCatalog>();
    }

    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddTransient<BearerTokenForwardingHandler>()
            .AddScoped<ITransferRepository, TransferRepository>()
            .AddScoped<IIdempotencyRepository, IdempotencyRepository>()
            .AddScoped<IIdempotencyHasher, IdempotencyHasher>();
    }

    public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
    {
        return services
            .AddTransient<IAccountApiClient, AccountApiClientAdapter>()
            .AddTransient<ITransferService, TransferService>();
    }

    public static IServiceCollection AddValidatorDependencies(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssemblyContaining<CreateTransferCommandValidator>();
    }

    public static IServiceCollection AddMediatrDependencies(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(CreateTransferCommandHandler)), // Application
        };

        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(IdempotencyBehavior<,>));
    }
}