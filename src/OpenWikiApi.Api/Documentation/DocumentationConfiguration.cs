using OpenWikiApi.Api.Documentation.SwaggerUI;

namespace OpenWikiApi.Api.Documentation;

public static class DocumentationConfiguration
{
    public static IServiceCollection AddDocumentationServices(
        this IServiceCollection services
    )
    {
        services.AddSwaggerDocServices();

        return services;
    }

    public static IApplicationBuilder UseDocumentationBuilder(
        this IApplicationBuilder app
    )
    {
        app.UseSwaggerDocBuilder();

        return app;
    }
}