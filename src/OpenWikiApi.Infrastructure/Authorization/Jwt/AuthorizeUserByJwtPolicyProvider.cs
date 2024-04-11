using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace OpenWikiApi.Infrastructure.Authorization.Jwt;

public class AuthorizeUserByJwtPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public AuthorizeUserByJwtPolicyProvider(
        IOptions<AuthorizationOptions> options
    ) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName
    )
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        string[] policyParts = policyName.Split("-");

        string[] roles = policyParts[0].Split(",");
        string permission = policyParts[1];

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new AuthorizeUserByJwtRequirement(roles, permission))
            .Build();
    }
}