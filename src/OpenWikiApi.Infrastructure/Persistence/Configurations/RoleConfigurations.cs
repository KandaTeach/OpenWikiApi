using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenWikiApi.Domain.Common.JointEntities;
using OpenWikiApi.Domain.Roles;
using OpenWikiApi.Domain.Roles.ValueObjects;

namespace OpenWikiApi.Infrastructure.Persistence.Configurations;

public sealed class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Domain.Roles.Role> builder)
    {
        ConfigureRoleTable(builder);
        ConfigurePermissionManyToManyRelationship(builder);
        ConfigureInitialDataForRole(builder);
    }

    private static void ConfigureRoleTable(EntityTypeBuilder<Role> builder)
    {
        builder
            .ToTable("Roles");

        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => RoleId.Create(value)
            );

        builder
            .Property(d => d.Name)
            .HasMaxLength(50);
    }

    private static void ConfigurePermissionManyToManyRelationship(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();
    }

    private static void ConfigureInitialDataForRole(EntityTypeBuilder<Role> builder)
    {
        IEnumerable<Role> roles = Enum
            .GetValues<Domain.Common.Constants.Role>()
            .Select(p => Role.Create(
                (int)p,
                p.ToString()
        ));

        builder
            .HasData(roles);
    }
}