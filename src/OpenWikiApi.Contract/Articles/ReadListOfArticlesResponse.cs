namespace OpenWikiApi.Contract.Articles;

public record ReadListOfArticlesResponse(
    List<ViewArticleResponse> Articles
);