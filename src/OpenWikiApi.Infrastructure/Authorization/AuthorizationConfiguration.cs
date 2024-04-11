using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using OpenWikiApi.Infrastructure.Authorization.Jwt;

namespace OpenWikiApi.Infrastructure.Authorization;

public static class AuthorizationConfiguration
{
    public static IServiceCollection AddAuthorizationServices(
        this IServiceCollection services
    )
    {
        // add middleware
        services.AddAuthorization();

        services.AddJsonWebToken();

        return services;
    }

    public static IApplicationBuilder UseAuthorizationBuilder(
        this IApplicationBuilder app
    )
    {
        app.UseAuthorization();

        return app;
    }

    private static IServiceCollection AddJsonWebToken(
        this IServiceCollection services
    )
    {
        // add handler
        services.AddSingleton<IAuthorizationHandler, AuthorizeUserByJwtHandler>();

        // add policy provider
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizeUserByJwtPolicyProvider>();

        return services;
    }
}