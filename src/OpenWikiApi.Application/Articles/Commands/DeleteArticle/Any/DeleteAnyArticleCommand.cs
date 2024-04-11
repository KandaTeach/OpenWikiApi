using ErrorOr;
using MediatR;

namespace OpenWikiApi.Application.Articles.Commands.DeleteArticle.Any;

public record DeleteAnyArticleCommand(
    string ArticleId
) : IRequest<ErrorOr<Unit>>;