using OpenWikiApi.Domain.Common.Models;

namespace OpenWikiApi.Domain.Articles.ValueObjects;

public sealed class ArticleId : ValueObject
{
    public Guid Value { get; private set; }

    private ArticleId(Guid value)
    {
        Value = value;
    }

    public static ArticleId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ArticleId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(ArticleId data)
        => data.Value;

    private ArticleId() { }
}