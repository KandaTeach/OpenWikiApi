using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticleList.Owned;

public record ReadOwnedArticleListQuery(
    string OwnerId
) : IRequest<ErrorOr<ArticleListResult>>;