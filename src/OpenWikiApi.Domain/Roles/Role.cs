using OpenWikiApi.Domain.Common.JointEntities;
using OpenWikiApi.Domain.Common.Models;
using OpenWikiApi.Domain.Permissions;
using OpenWikiApi.Domain.Roles.ValueObjects;

namespace OpenWikiApi.Domain.Roles;

public sealed class Role : AggregateRoot<RoleId>
{
    private readonly List<Permission> _permissions = new();

    private readonly List<UserRole> _userRoles = new();

    private readonly List<RolePermission> _rolePermissions = new();

    public string Name { get; private set; }

    public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();

    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    public Role(
        RoleId id,
        string name
    ) : base(id)
    {
        Name = name;
    }

    public static Role Create(
        int id,
        string name
    )
    {
        return new Role(
            RoleId.Create(id),
            name
        );
    }

    public void AddPermission(Permission? permission)
    {
        _permissions.Add(permission!);
    }

    public void AddPermissionRange(List<Permission>? permissions)
    {
        _permissions.AddRange(permissions!);
    }

    public void AddRolePermission(RolePermission? rolePermission)
    {
        _rolePermissions.Add(rolePermission!);
    }

    public void AddRolePermissionRange(List<RolePermission>? rolePermissions)
    {
        _rolePermissions.AddRange(rolePermissions!);
    }

#pragma warning disable CS8618
    private Role() { }
#pragma warning restore CS8618

}