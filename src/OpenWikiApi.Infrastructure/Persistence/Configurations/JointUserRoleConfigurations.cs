using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenWikiApi.Domain.Common.JointEntities;

namespace OpenWikiApi.Infrastructure.Persistence.Configurations;

public sealed class JointUserRoleConfigurations : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        ConfigureUserRoleJointTable(builder);
    }

    private static void ConfigureUserRoleJointTable(EntityTypeBuilder<UserRole> builder)
    {
        builder
            .ToTable("UserRoles");

        builder
            .HasKey(x => new { x.UserId, x.RoleId });

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}