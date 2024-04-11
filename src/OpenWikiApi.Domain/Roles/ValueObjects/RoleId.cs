using OpenWikiApi.Domain.Common.Models;

namespace OpenWikiApi.Domain.Roles.ValueObjects;

public sealed class RoleId : ValueObject
{
    public int Value { get; private set; }

    private RoleId(int value)
    {
        Value = value;
    }

    public static RoleId Create(int value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(RoleId data)
        => data.Value;

    private RoleId() { }
}