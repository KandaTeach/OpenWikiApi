using OpenWikiApi.Api;
using OpenWikiApi.Application;
using OpenWikiApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UsePresentation();
    app.UseInfrastructure();

    app.MapControllers();
    app.Run();
}

