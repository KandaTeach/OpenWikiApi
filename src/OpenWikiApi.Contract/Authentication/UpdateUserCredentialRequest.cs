namespace OpenWikiApi.Contract.Authentication;

public record UpdateUserCredentialRequest(
    string Username,
    string Password
);