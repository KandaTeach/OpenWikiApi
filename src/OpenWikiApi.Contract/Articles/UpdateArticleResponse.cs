namespace OpenWikiApi.Contract.Articles;

public record UpdateArticleResponse(
    Guid UpdateId,
    string Title,
    string Content,
    List<string> Reference,
    string UpdateOwner,
    ReferenceArticleResponse FromThisArticle,
    DateTime UpdatedDateTime
);

public record ReferenceArticleResponse(
    Guid Id,
    string Title
);