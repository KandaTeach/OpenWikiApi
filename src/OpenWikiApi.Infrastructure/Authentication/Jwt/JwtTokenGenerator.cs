using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using OpenWikiApi.Application.Common.Interfaces.Services.Authentication;
using OpenWikiApi.Domain.Users;
using OpenWikiApi.Infrastructure.Persistence.Context;

namespace OpenWikiApi.Infrastructure.Authentication.Jwt;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(
        IOptions<JwtSettings> jwtOptions,
        OpenWikiApiDbContext context
    )
    {
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateTokenAsync(User user)
    {
        var roles = user.Roles
            .Select(r => r.Name)
            .ToHashSet();

        var permissions = user.Roles
            .SelectMany(r => r.Permissions)
            .Select(p => p.Name)
            .ToHashSet();

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (string role in roles)
        {
            claims.Add(new("roles", role));
        }

        foreach (string permission in permissions)
        {
            claims.Add(new("permissions", permission));
        }

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        string token = new JwtSecurityTokenHandler()
            .WriteToken(securityToken);

        return token;
    }
}
