using Microsoft.Extensions.DependencyInjection;
using Transfer.Core.Common.Indepotencies.Hashing;
using Transfer.Core.Common.Indepotencies.Repositories;
using Transfer.Infrastructure.Common.Idempotencies.Hashing;
using Transfer.Infrastructure.Common.Idempotencies.Repositories;
using Transfer.Infrastructure.CrossCutting.ResourcesCatalog;

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
            .AddScoped<IIdempotencyRepository, IdempotencyRepository>()
            .AddScoped<IIdempotencyHasher, IdempotencyHasher>();
    }

    public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddValidatorDependencies(this IServiceCollection services)
    {
        return services;
        //.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
    }

    public static IServiceCollection AddMediatrDependencies(this IServiceCollection services)
    {
        //var mediatRAssemblies = new[]{};
        //{
        //    Assembly.GetAssembly(typeof(CreateUserCommandHandler)), // Application
        //};

        //return services
        //    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
        //    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>))
        //    .AddTransient(typeof(IPipelineBehavior<,>), typeof(IdempotencyBehavior<,>));

        return services;
    }
}