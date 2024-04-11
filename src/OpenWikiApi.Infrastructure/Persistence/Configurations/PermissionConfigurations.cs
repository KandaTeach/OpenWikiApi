using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenWikiApi.Domain.Permissions;
using OpenWikiApi.Domain.Permissions.ValueObjects;

namespace OpenWikiApi.Infrastructure.Persistence.Configurations;

public sealed class PermissionConfigurations : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        ConfigurePermissionTable(builder);
        ConfigureInitialDataForPermission(builder);
    }

    private static void ConfigurePermissionTable(EntityTypeBuilder<Permission> builder)
    {
        builder
            .ToTable("Permissions");

        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => PermissionId.Create(value)
            );

        builder
            .Property(u => u.Name)
            .HasMaxLength(50);
    }
    private static void ConfigureInitialDataForPermission(EntityTypeBuilder<Permission> builder)
    {
        IEnumerable<Permission> permissions = Enum
            .GetValues<Domain.Common.Constants.Permission>()
            .Select(p => Permission.Create(
                (int)p,
                p.ToString()
        ));

        builder
            .HasData(permissions);
    }
}