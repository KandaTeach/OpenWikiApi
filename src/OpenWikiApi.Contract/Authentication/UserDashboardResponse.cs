namespace OpenWikiApi.Contract.Authentication;

public record UserDashboardResponse(
    UserInformationResponse User,
    Dictionary<Guid, string> Articles
);

public record UserInformationResponse(
    Guid Id,
    string Nickname,
    UserProfileResponse Profile,
    UserCredentialResponse Credential,
    List<string> Roles
);

public record UserProfileResponse(
    string Name,
    int Age,
    string Email
);

public record UserCredentialResponse(
    string Username
);