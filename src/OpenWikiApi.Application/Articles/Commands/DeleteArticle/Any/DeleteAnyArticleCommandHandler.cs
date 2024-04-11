using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Common.Errors;

namespace OpenWikiApi.Application.Articles.Commands.DeleteArticle.Any;

public class DeleteAnyArticleCommandHandler : IRequestHandler<DeleteAnyArticleCommand, ErrorOr<Unit>>
{
    private readonly IArticleRepository _articleRepo;

    public DeleteAnyArticleCommandHandler(
        IArticleRepository articleRepo
    )
    {
        _articleRepo = articleRepo;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteAnyArticleCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.ArticleId, out Guid parsedArticleId))
        {
            return Errors.Article.InvalidArticle;
        }

        var article = await _articleRepo.GetArticleByIdAsync(ArticleId.Create(parsedArticleId));

        if (article is null)
        {
            return Errors.Article.InvalidArticle;
        }

        await _articleRepo.DeleteArticleAsync(article);

        return Unit.Value;
    }
}
