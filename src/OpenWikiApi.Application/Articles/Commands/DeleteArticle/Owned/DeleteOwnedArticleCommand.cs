using ErrorOr;
using MediatR;

namespace OpenWikiApi.Application.Articles.Commands.DeleteArticle.Owned;

public record DeleteOwnedArticleCommand(
    string ArticleId,
    string OwnerId
) : IRequest<ErrorOr<Unit>>;