using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Articles.Entities.ArticleUpdates;
using OpenWikiApi.Domain.Articles.Entities.ArticleUpdates.ValueObjects;
using OpenWikiApi.Domain.Articles.ValueObjects;

namespace OpenWikiApi.Infrastructure.Persistence.Configurations;

public sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        ConfigureArticleTable(builder);
        ConfigureArticleUpdateTable(builder);
    }

    private static void ConfigureArticleTable(EntityTypeBuilder<Article> builder)
    {
        builder
            .ToTable("Articles");

        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ArticleId.Create(value));

        builder
            .Property(u => u.Title)
            .HasMaxLength(200);

        builder
            .Property(u => u.Content)
            .HasMaxLength(5000);

        builder
            .Property(u => u.Reference)
            .HasMaxLength(1000)
            .HasConversion(
                r => JsonSerializer.Serialize(r, (JsonSerializerOptions)null!),
                r => JsonSerializer.Deserialize<List<string>>(r, (JsonSerializerOptions)null!)!);

        builder
            .Property(u => u.Reference)
            .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));

    }

    private static void ConfigureArticleUpdateTable(EntityTypeBuilder<Article> builder)
    {
        builder.OwnsMany(e => e.Updates, articleUpdateBuilder =>
        {
            articleUpdateBuilder
                .ToTable("ArticleUpdates");

            articleUpdateBuilder
                .WithOwner()
                .HasForeignKey(nameof(ArticleId));

            articleUpdateBuilder
                .HasKey(nameof(ArticleUpdate.Id), nameof(ArticleId));

            articleUpdateBuilder
                .Property(u => u.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ArticleUpdateId.Create(value));

            articleUpdateBuilder
                .Property(u => u.Title)
                .HasMaxLength(200);

            articleUpdateBuilder
                .Property(u => u.Content)
                .HasMaxLength(5000);

            articleUpdateBuilder
                .Property(u => u.Reference)
                .HasMaxLength(1000)
                .HasConversion(
                    r => JsonSerializer.Serialize(r, (JsonSerializerOptions)null!),
                    r => JsonSerializer.Deserialize<List<string>>(r, (JsonSerializerOptions)null!)!);

            articleUpdateBuilder
                .Property(u => u.Reference)
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

            articleUpdateBuilder
                .HasOne(x => x.UpdateOwner)
                .WithMany()
                .HasForeignKey(x => x.UpdateOwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Metadata
            .FindNavigation(nameof(Article.Updates))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

}