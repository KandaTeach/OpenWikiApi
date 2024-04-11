using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenWikiApi.Domain.Users;
using OpenWikiApi.Domain.Users.ValueObjects;
using OpenWikiApi.Domain.Users.Entities.UserProfiles.ValueObjects;
using OpenWikiApi.Domain.Users.Entities.UserProfiles;
using OpenWikiApi.Domain.Common.JointEntities;
using OpenWikiApi.Domain.Users.Entities.UserCredentials;
using OpenWikiApi.Domain.Users.Entities.UserCredentials.ValueObjects;

namespace OpenWikiApi.Infrastructure.Persistence.Configurations;

public sealed class UserAccountConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigureUserCredentialTable(builder);
        ConfigureUserProfileTable(builder);
        ConfigureRoleAndUserRelationship(builder);
        ConfigureArticleAndUserRelationship(builder);
    }

    private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder
            .Property(u => u.Nickname)
            .HasMaxLength(15);
    }

    private static void ConfigureUserCredentialTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(e => e.Credential, userCredentialBuilder =>
        {
            userCredentialBuilder
                .ToTable("UserCredentials");

            userCredentialBuilder
                .WithOwner()
                .HasForeignKey(nameof(UserId));

            userCredentialBuilder
                .HasKey(nameof(UserCredential.Id), nameof(UserId));

            userCredentialBuilder
                .Property(u => u.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserCredentialId.Create(value)
                );

            userCredentialBuilder
                .Property(u => u.Username)
                .HasMaxLength(15);

            userCredentialBuilder
                .Property(u => u.Password)
                .HasMaxLength(25);
        });

        builder.Metadata
            .FindNavigation(nameof(User.Credential))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureUserProfileTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(e => e.Profile, userProfileBuilder =>
        {
            userProfileBuilder
                .ToTable("UserProfiles");

            userProfileBuilder
                .WithOwner()
                .HasForeignKey(nameof(UserId));

            userProfileBuilder
                .HasKey(nameof(UserProfile.Id), nameof(UserId));

            userProfileBuilder
                .Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserProfileId.Create(value)
                );

            userProfileBuilder
                .Property(p => p.Name)
                .HasMaxLength(100);

            userProfileBuilder
                .Property(p => p.Age)
                .HasMaxLength(100);

            userProfileBuilder
                .Property(p => p.Email)
                .HasMaxLength(100);
        });

        builder.Metadata
            .FindNavigation(nameof(User.Profile))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureRoleAndUserRelationship(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(x => x.Roles)
            .WithMany()
            .UsingEntity<UserRole>();
    }

    private static void ConfigureArticleAndUserRelationship(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(x => x.Articles)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId);
    }

}