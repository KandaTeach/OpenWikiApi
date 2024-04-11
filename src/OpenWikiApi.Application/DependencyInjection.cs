using Microsoft.Extensions.DependencyInjection;
using OpenWikiApi.Application.Common;

namespace OpenWikiApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
    {
        services.AddCommonServices();

        return services;
    }
}