using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using OpenWikiApi.Application.Common.Interfaces.Services.Authentication;
using OpenWikiApi.Infrastructure.Authentication.Jwt;

namespace OpenWikiApi.Infrastructure.Authentication;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddAuthenticationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddJsonWebtoken(configuration);

        return services;
    }

    public static IApplicationBuilder UseAuthenticationBuilder(
        this IApplicationBuilder app
    )
    {
        app.UseAuthentication();

        return app;
    }

    private static IServiceCollection AddJsonWebtoken(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // add generator
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        // bind settings
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        // add middleware
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    AuthenticationType = JwtSettings.TypeOfAuthentication,
                };
            });

        return services;
    }
}