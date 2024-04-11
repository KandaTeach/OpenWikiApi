using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticle;

public record ReadArticleQuery(
    string ArticleId
) : IRequest<ErrorOr<ArticleResult>>;