using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenWikiApi.Infrastructure.Authentication;
using OpenWikiApi.Infrastructure.Authorization;
using OpenWikiApi.Infrastructure.Persistence;

namespace OpenWikiApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddPersistenceServices(configuration)
            .AddAuthenticationServices(configuration)
            .AddAuthorizationServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(
        this IApplicationBuilder app
    )
    {
        app.UseAuthenticationBuilder()
            .UseAuthorizationBuilder();

        return app;
    }
}