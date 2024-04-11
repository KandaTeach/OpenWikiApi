using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenWikiApi.Domain.Common.Constants;
using OpenWikiApi.Domain.Common.JointEntities;
using OpenWikiApi.Domain.Permissions.ValueObjects;
using OpenWikiApi.Domain.Roles.ValueObjects;

namespace OpenWikiApi.Infrastructure.Persistence.Configurations;

public sealed class JointRolePermissionConfigurations : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        ConfigureRolePermissionJointTable(builder);
        ConfigureInitialDataForRolePermissionJointTable(builder);
    }

    private static void ConfigureRolePermissionJointTable(EntityTypeBuilder<RolePermission> builder)
    {
        builder
            .ToTable("RolePermissions");

        builder
            .HasKey(x => new { x.RoleId, x.PermissionId });

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Permission)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureInitialDataForRolePermissionJointTable(EntityTypeBuilder<RolePermission> builder)
    {
        IEnumerable<RoleId> roleIds = Enum
            .GetValues<Role>()
            .Select(p => RoleId.Create(
                (int)p
            )
        );

        IEnumerable<PermissionId> permissionIds = Enum
            .GetValues<Permission>()
            .Select(p => PermissionId.Create(
                (int)p
            )
        );

        builder
            .HasData(RolePermission.Create(roleIds, permissionIds));
    }

}