namespace OpenWikiApi.Contract.Articles;

public record AddArticleRequest(
    string Title,
    string Content,
    List<string> Reference
);