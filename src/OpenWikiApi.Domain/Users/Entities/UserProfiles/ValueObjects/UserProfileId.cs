using OpenWikiApi.Domain.Common.Models;

namespace OpenWikiApi.Domain.Users.Entities.UserProfiles.ValueObjects;

public sealed class UserProfileId : ValueObject
{
    public Guid Value { get; private set; }

    private UserProfileId(Guid value)
    {
        Value = value;
    }

    public static UserProfileId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UserProfileId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(UserProfileId data)
        => data.Value;

    private UserProfileId() { }
}