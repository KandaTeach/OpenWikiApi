using OpenWikiApi.Domain.Common.Models;
using OpenWikiApi.Domain.Users.Entities.UserCredentials.ValueObjects;

namespace OpenWikiApi.Domain.Users.Entities.UserCredentials;

public sealed class UserCredential : Entity<UserCredentialId>
{
    public string Username { get; private set; }
    public string Password { get; private set; }

    private UserCredential(
        UserCredentialId id,
        string username,
        string password
    ) : base(id)
    {
        Username = username;
        Password = password;
    }

    public static UserCredential Create(
        string username,
        string password
    )
    {
        return new(
            UserCredentialId.CreateUnique(),
            username,
            password
        );
    }

#pragma warning disable CS8618
    private UserCredential() { }
#pragma warning restore CS8618
}