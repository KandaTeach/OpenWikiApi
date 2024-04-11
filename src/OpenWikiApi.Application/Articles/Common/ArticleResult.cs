using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Articles.Entities.ArticleUpdates;

namespace OpenWikiApi.Application.Articles.Common;

public record ArticleResult(
    Article Article,
    ArticleUpdate Update
);