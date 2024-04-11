using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;

namespace OpenWikiApi.Application.Articles.Commands.InsertArticle;

public record InsertArticleCommand(
    string Title,
    string Content,
    List<string> Reference,
    string OwnerId
) : IRequest<ErrorOr<ArticleResult>>
{
    public InsertArticleCommand MapOwnerId(string ownerId) =>
        this with { OwnerId = ownerId };
};