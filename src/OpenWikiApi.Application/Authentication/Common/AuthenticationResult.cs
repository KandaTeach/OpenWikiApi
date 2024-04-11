using OpenWikiApi.Domain.Users;

namespace OpenWikiApi.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);