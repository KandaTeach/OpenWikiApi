using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticleUpdateList;

public record ReadArticleUpdateListQuery(
    string ArticleId
) : IRequest<ErrorOr<ArticleListResult>>;