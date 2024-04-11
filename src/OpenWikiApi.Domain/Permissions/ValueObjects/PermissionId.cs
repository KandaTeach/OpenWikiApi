using OpenWikiApi.Domain.Common.Models;

namespace OpenWikiApi.Domain.Permissions.ValueObjects;

public sealed class PermissionId : ValueObject
{
    public int Value { get; private set; }

    private PermissionId(int value)
    {
        Value = value;
    }

    public static PermissionId Create(int value)
    {
        return new PermissionId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(PermissionId data)
        => data.Value;

    private PermissionId() { }
}