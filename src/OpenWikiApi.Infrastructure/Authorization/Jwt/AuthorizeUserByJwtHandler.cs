using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace OpenWikiApi.Infrastructure.Authorization.Jwt;

public class AuthorizeUserByJwtHandler : AuthorizationHandler<AuthorizeUserByJwtRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeUserByJwtRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(x =>
            x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var roles = context.User.Claims
            .Where(x => x.Type == ClaimTypes.Role)
            .Select(x => x.Value)
            .ToHashSet(); ;

        var permissions = context.User.Claims
            .Where(x => x.Type == "permissions")
            .Select(x => x.Value)
            .ToHashSet();

        foreach (var role in requirement.Role)
        {
            if (roles.Contains(role) && permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        context.Fail();
        return Task.CompletedTask;
    }
}