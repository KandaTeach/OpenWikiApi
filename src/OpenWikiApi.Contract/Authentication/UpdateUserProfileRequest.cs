namespace OpenWikiApi.Contract.Authentication;

public record UpdateUserProfileRequest(
    string Name,
    int Age,
    string Email
);