using ErrorOr;

namespace OpenWikiApi.Domain.Common.Errors;

public static partial class Errors
{
    public static class Article
    {
        public static Error InvalidArticle =>
            Error.NotFound(
                code: "Article.InvalidArticle",
                description: "Article does not exist."
            );

        public static Error ArticleNotOwned =>
            Error.Conflict(
                code: "Article.ArticleNotOwned",
                description: "Article does not exist or is not owned by you."
            );

        public static Error NoArticles =>
            Error.NotFound(
                code: "Article.NoArticles",
                description: "No articles have been made for this user."
            );

        public static Error NoUpdates =>
            Error.NotFound(
                code: "Article.NoUpdates",
                description: "No updates for this article."
            );
    }
}