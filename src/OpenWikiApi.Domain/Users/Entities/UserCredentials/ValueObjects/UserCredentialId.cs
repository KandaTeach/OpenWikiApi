using OpenWikiApi.Domain.Common.Models;

namespace OpenWikiApi.Domain.Users.Entities.UserCredentials.ValueObjects;

public sealed class UserCredentialId : ValueObject
{
    public Guid Value { get; private set; }

    private UserCredentialId(Guid value)
    {
        Value = value;
    }

    public static UserCredentialId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UserCredentialId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(UserCredentialId data)
        => data.Value;

    private UserCredentialId() { }
}