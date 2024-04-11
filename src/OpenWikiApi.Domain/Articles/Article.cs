using OpenWikiApi.Domain.Articles.Entities.ArticleUpdates;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Common.Models;
using OpenWikiApi.Domain.Users;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Domain.Articles;

public sealed class Article : AggregateRoot<ArticleId>
{
    private readonly List<ArticleUpdate> _updates = new();

    public string Title { get; private set; }
    public string Content { get; private set; }

    public List<string> Reference { get; private set; }
    public DateTime CreatedDateTime { get; private set; }

    public UserId OwnerId { get; private set; } = null!;
    public User Owner { get; private set; } = null!;
    public IReadOnlyList<ArticleUpdate> Updates => _updates.AsReadOnly();

    private Article(
        ArticleId id,
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

    public static Article Create(
        string title,
        string content,
        List<string> reference
    )
    {
        return new(
            ArticleId.CreateUnique(),
            title,
            content,
            reference
        );
    }

    public void AddOwner(User? owner)
    {
        Owner = owner!;
    }

    public void UpdateArticle(ArticleUpdate? update)
    {
        _updates.Add(update!);
    }

#pragma warning disable CS8618
    private Article() { }
#pragma warning restore CS8618

}