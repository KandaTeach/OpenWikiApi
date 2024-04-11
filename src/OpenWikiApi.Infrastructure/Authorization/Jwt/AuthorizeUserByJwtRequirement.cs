using Microsoft.AspNetCore.Authorization;

namespace OpenWikiApi.Infrastructure.Authorization.Jwt;

public class AuthorizeUserByJwtRequirement : IAuthorizationRequirement
{
    public string[] Role { get; }
    public string Permission { get; }

    public AuthorizeUserByJwtRequirement(
        string[] role,
        string permission
    )
    {
        Role = role;
        Permission = permission;
    }
}