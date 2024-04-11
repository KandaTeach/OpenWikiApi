using OpenWikiApi.Domain.Common.Models;
using OpenWikiApi.Domain.Users.Entities.UserProfiles.ValueObjects;

namespace OpenWikiApi.Domain.Users.Entities.UserProfiles;

public sealed class UserProfile : Entity<UserProfileId>
{
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Email { get; private set; }

    private UserProfile(
        UserProfileId id,
        string name,
        int age,
        string email
    ) : base(id)
    {
        Name = name;
        Age = age;
        Email = email;
    }

    public static UserProfile Create(
        string name,
        int age,
        string email
    )
    {
        return new(
            UserProfileId.CreateUnique(),
            name,
            age,
            email
        );
    }

#pragma warning disable CS8618
    private UserProfile() { }
#pragma warning restore CS8618
}