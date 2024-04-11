using OpenWikiApi.Domain.Common.JointEntities;
using OpenWikiApi.Domain.Common.Models;
using OpenWikiApi.Domain.Permissions.ValueObjects;

namespace OpenWikiApi.Domain.Permissions;

public sealed class Permission : AggregateRoot<PermissionId>
{
    private readonly List<RolePermission> _rolePermissions = new();

    public string Name { get; private set; }

    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    public Permission(
        PermissionId id,
        string name
    ) : base(id)
    {
        Name = name;
    }

    public static Permission Create(
        int id,
        string name
    )
    {
        return new Permission(
            PermissionId.Create(id),
            name
        );
    }

#pragma warning disable CS8618
    private Permission() { }
#pragma warning restore CS8618

}