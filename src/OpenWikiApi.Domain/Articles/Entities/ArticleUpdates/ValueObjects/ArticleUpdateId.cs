using OpenWikiApi.Domain.Common.Models;

namespace OpenWikiApi.Domain.Articles.Entities.ArticleUpdates.ValueObjects;

public sealed class ArticleUpdateId : ValueObject
{
    public Guid Value { get; private set; }

    private ArticleUpdateId(Guid value)
    {
        Value = value;
    }

    public static ArticleUpdateId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ArticleUpdateId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(ArticleUpdateId data)
        => data.Value;

    private ArticleUpdateId() { }
}