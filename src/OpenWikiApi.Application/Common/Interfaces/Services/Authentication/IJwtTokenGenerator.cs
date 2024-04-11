using OpenWikiApi.Domain.Users;

namespace OpenWikiApi.Application.Common.Interfaces.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateTokenAsync(User user);
}