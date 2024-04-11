namespace OpenWikiApi.Contract.Articles;

public record AddArticleResponse(
    Guid ArticleId,
    string Title,
    string Content,
    List<string> Reference,
    string ArticleAuthor,
    DateTime CreatedDateTime
);