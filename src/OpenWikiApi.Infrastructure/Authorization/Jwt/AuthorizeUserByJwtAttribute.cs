using Microsoft.AspNetCore.Authorization;

using OpenWikiApi.Domain.Common.Constants;

namespace OpenWikiApi.Infrastructure.Authorization.Jwt;

public sealed class AuthorizeUserByJwtAttribute : AuthorizeAttribute
{
    public AuthorizeUserByJwtAttribute(Role[] roles, Permission permission)
        : base(policy: string.Join(",", roles) + "-" + permission.ToString())
    {
    }
}