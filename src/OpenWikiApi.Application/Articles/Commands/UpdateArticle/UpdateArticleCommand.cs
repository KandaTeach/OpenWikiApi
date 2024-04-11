using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;

namespace OpenWikiApi.Application.Articles.Commands.UpdateArticle;

public record UpdateArticleCommand(
    string ArticleId,
    string Title,
    string Content,
    List<string> Reference,
    string UpdateOwnerId
) : IRequest<ErrorOr<ArticleResult>>
{
    public UpdateArticleCommand MapUpdateOwnerId(string updateOwnerId) =>
        this with { UpdateOwnerId = updateOwnerId };
};