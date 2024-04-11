using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Articles.Commands.DeleteArticle.Owned;

public class DeleteOwnedArticleCommandHandler : IRequestHandler<DeleteOwnedArticleCommand, ErrorOr<Unit>>
{
    private readonly IArticleRepository _articleRepo;
    private readonly IAuthenticationRepository _authRepo;

    public DeleteOwnedArticleCommandHandler(
        IArticleRepository articleRepo,
        IAuthenticationRepository authRepo
    )
    {
        _articleRepo = articleRepo;
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteOwnedArticleCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.OwnerId, out Guid parsedOwnerId))
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var owner = await _authRepo.GetUserById(UserId.Create(parsedOwnerId));

        if (owner is null)
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        if (!Guid.TryParse(request.ArticleId, out Guid parsedArticleId))
        {
            return Errors.Article.ArticleNotOwned;
        }

        var article = await _articleRepo.GetUserOwnedArticleByIdAsync(ArticleId.Create(parsedArticleId), owner.Id);

        if (article is null)
        {
            return Errors.Article.ArticleNotOwned;
        }

        await _articleRepo.DeleteArticleAsync(article);

        return Unit.Value;
    }
}