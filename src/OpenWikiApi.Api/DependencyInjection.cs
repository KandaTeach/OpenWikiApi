using OpenWikiApi.Api.Common;
using OpenWikiApi.Api.Documentation;

namespace OpenWikiApi.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services
    )
    {
        services.AddControllers();

        services.AddCommonServices()
            .AddDocumentationServices();

        return services;
    }

    public static IApplicationBuilder UsePresentation(
        this IApplicationBuilder app
    )
    {
        app.UseDocumentationBuilder();
        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();

        return app;
    }
}