namespace OpenWikiApi.Contract.Articles;

public record ReadArticleResponse(
    string Title,
    string Content,
    List<string> Reference,
    ViewUpdatedArticleResponse UpdateInformation,
    ViewArticleSourceResponse ArticleSource,
    List<ViewArticleUpdateResponse> ListOfUpdates
);

public record ViewArticleSourceResponse(
    Guid ArticleId,
    string Author,
    DateTime CreatedOn
);

public record ViewUpdatedArticleResponse(
    Guid UpdateId,
    string EditedBy,
    DateTime UpdatedOn
);