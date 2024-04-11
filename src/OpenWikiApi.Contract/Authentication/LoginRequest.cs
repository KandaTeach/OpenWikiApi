namespace OpenWikiApi.Contract.Authentication;

public record LoginRequest(
    string Username,
    string Password
);