namespace OpenWikiApi.Contract.Authentication;

public record LoginResponse(
    Guid Id,
    string Nickname,
    string Name,
    int Age,
    string Email,
    string Token
);