using System.Reflection;

using FluentValidation;
using MediatR;

using Microsoft.Extensions.DependencyInjection;

using OpenWikiApi.Application.Common.Behaviors;

namespace OpenWikiApi.Application.Common;

public static class CommonConfiguration
{
    public static IServiceCollection AddCommonServices(
        this IServiceCollection services
    )
    {
        services
            .AddMediator()
            .AddValidation();

        return services;
    }

    private static IServiceCollection AddMediator(
        this IServiceCollection services
    )
    {
        // Add query and command mediator dynamically 
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly)
        );

        return services;
    }

    private static IServiceCollection AddValidation(
        this IServiceCollection services
    )
    {
        // Add validator behavior dynamically
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

        // Add query and command validators dynamically
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}