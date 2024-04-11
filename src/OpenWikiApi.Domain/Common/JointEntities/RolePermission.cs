using OpenWikiApi.Domain.Permissions.ValueObjects;
using OpenWikiApi.Domain.Roles.ValueObjects;

namespace OpenWikiApi.Domain.Common.JointEntities;

public class RolePermission
{
    public RoleId RoleId { get; set; } = null!;
    public Roles.Role Role { get; set; } = null!;
    public PermissionId PermissionId { get; set; } = null!;
    public Permissions.Permission Permission { get; set; } = null!;

    public static RolePermission Create(
        Constants.Role roleId,
        Constants.Permission permissionId
    )
    {
        return new RolePermission
        {
            RoleId = RoleId.Create((int)roleId),
            PermissionId = PermissionId.Create((int)permissionId)
        };
    }

    public static IEnumerable<RolePermission> Create(
        IEnumerable<RoleId> roleIds,
        IEnumerable<PermissionId> permissionIds
    )
    {
        foreach (var roleId in roleIds)
        {
            foreach (var permissionId in permissionIds)
            {
                yield return new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };
            }
        }
    }
}