namespace OpenWikiApi.Contract.Authentication;

public record RegisterResponse(
    Guid Id,
    string Nickname,
    string Name,
    int Age,
    string Email
);