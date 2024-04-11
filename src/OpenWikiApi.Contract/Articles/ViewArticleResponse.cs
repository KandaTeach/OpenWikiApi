namespace OpenWikiApi.Contract.Articles;

public record ViewArticleResponse(
    Guid ArticleId,
    string Title
);