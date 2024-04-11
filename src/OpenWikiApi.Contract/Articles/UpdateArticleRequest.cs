namespace OpenWikiApi.Contract.Articles;

public record UpdateArticleRequest(
    string ArticleId,
    string Title,
    string Content,
    List<string> Reference
);