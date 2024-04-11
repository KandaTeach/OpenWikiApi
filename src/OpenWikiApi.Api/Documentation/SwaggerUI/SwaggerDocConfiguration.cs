using System.Reflection;
using Microsoft.OpenApi.Models;

namespace OpenWikiApi.Api.Documentation.SwaggerUI;

public static class SwaggerDocConfiguration
{
    public static IServiceCollection AddSwaggerDocServices(
        this IServiceCollection services
    )
    {
        services.AddSwaggerGeneralInformation();

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocBuilder(
        this IApplicationBuilder app
    )
    {
        app.UseSwaggerUIBuilder();

        return app;
    }

    private static IServiceCollection AddSwaggerGeneralInformation(
        this IServiceCollection services
    )
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "OpenWikiApi",
                Description = "A simple open source wiki api written in ASP.NET Core",
                Version = "v1"
            });

            options.AddSecurityDefinition("JWT Bearer Authentication", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Place the generated JWT token to authenticate.",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "JWT Bearer Authentication",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] { }
                },
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    private static IApplicationBuilder UseSwaggerUIBuilder(
        this IApplicationBuilder app
    )
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        return app;
    }
}