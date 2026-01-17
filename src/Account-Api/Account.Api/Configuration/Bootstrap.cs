using Account.Core.Repositories.Bases;
using Account.Infrastructure.CrossCutting.ResourcesCatalog;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Api.Configuration;

public static class Bootstrap
{
    public static IServiceCollection AddResourcesDependencies(this IServiceCollection services)
    {
        return services.AddSingleton<IResourceCatalog, ResourceCatalog>();
    }

    public static IServiceCollection AddRepositoriesDependencies(this IServiceCollection services)
    {
        return services;
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
        //    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

        return services;
    }
}