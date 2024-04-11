namespace OpenWikiApi.Contract.Articles;

public record ViewArticleUpdateResponse(
    Guid UpdateId,
    string Title
);