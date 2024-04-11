using OpenWikiApi.Domain.Articles.Entities.ArticleUpdates.ValueObjects;
using OpenWikiApi.Domain.Common.Models;
using OpenWikiApi.Domain.Users;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Domain.Articles.Entities.ArticleUpdates;

public sealed class ArticleUpdate : Entity<ArticleUpdateId>
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public List<string> Reference { get; private set; }
    public DateTime CreatedDateTime { get; private set; }

    public UserId UpdateOwnerId { get; private set; } = null!;
    public User UpdateOwner { get; private set; } = null!;

    private ArticleUpdate(
        ArticleUpdateId id,
        string title,
        string content,
        List<string> reference
    ) : base(id)
    {
        Title = title;
        Content = content;
        Reference = reference;
        CreatedDateTime = DateTime.Now;
    }

    public static ArticleUpdate Create(
        string title,
        string content,
        List<string> reference
    )
    {
        return new(
            ArticleUpdateId.CreateUnique(),
            title,
            content,
            reference
        );
    }

    public void AddUpdateOwner(User? updateOwner)
    {
        UpdateOwner = updateOwner!;
    }

#pragma warning disable CS8618
    private ArticleUpdate() { }
#pragma warning restore CS8618

}