using Microsoft.EntityFrameworkCore;

using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Roles;
using OpenWikiApi.Domain.Users;

namespace OpenWikiApi.Infrastructure.Persistence.Context;

public sealed class OpenWikiApiDbContext : DbContext
{
    public OpenWikiApiDbContext(
        DbContextOptions<OpenWikiApiDbContext> options
    ) : base(options)
    {

    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(OpenWikiApiDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}