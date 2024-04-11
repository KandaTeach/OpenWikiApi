namespace OpenWikiApi.Contract.Authentication;

public record RegisterRequest(
    string Nickname,
    RegisterCredentialRequest Credential,
    RegisterProfileRequest Profile
);

public record RegisterCredentialRequest(
    string Username,
    string Password
);

public record RegisterProfileRequest(
    string Name,
    int Age,
    string Email
);