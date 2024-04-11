namespace OpenWikiApi.Contract.Articles;

public record ReadListOfArticleUpdatesResponse(
    List<ViewArticleUpdateResponse> ArticleUpdates
);