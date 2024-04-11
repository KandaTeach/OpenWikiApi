using System.Reflection;

using Mapster;
using MapsterMapper;

using Microsoft.AspNetCore.Mvc.Infrastructure;

using OpenWikiApi.Api.Common.Errors;

namespace OpenWikiApi.Api.Common;

public static class CommonConfiguration
{
    public static IServiceCollection AddCommonServices(
        this IServiceCollection services
    )
    {
        services
            .AddProblemDetailsFactory()
            .AddMappings();

        return services;
    }

    private static IServiceCollection AddProblemDetailsFactory(
        this IServiceCollection services
    )
    {
        // add the custom problem details factory
        services.AddSingleton<ProblemDetailsFactory, CustomlyProblemDetailsFactory>();

        return services;
    }

    private static IServiceCollection AddMappings(
        this IServiceCollection services
    )
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}