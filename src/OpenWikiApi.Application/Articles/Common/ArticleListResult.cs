using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Articles.Entities.ArticleUpdates;

namespace OpenWikiApi.Application.Articles.Common;

public record ArticleListResult(
    List<Article> Articles,
    List<ArticleUpdate> Updates
);