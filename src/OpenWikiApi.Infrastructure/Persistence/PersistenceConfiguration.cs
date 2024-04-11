using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Infrastructure.Persistence.Context;
using OpenWikiApi.Infrastructure.Persistence.Repository;

namespace OpenWikiApi.Infrastructure.Persistence;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddDatabaseContext(configuration)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddDatabaseContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<OpenWikiApiDbContext>(options =>
            options.UseSqlServer(configuration["DefaultConnection:ConnectionString"]));

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();

        return services;
    }
}