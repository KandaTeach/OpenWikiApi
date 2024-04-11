using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Articles.Entities.ArticleUpdates;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Articles.Commands.UpdateArticle;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ErrorOr<ArticleResult>>
{
    private readonly IArticleRepository _articleRepo;
    private readonly IAuthenticationRepository _authRepo;

    public UpdateArticleCommandHandler(
        IArticleRepository articleRepo,
        IAuthenticationRepository authRepo
    )
    {
        _articleRepo = articleRepo;
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<ArticleResult>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UpdateOwnerId, out Guid parsedUpdateOwnerId))
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var updateOwner = await _authRepo.GetUserById(UserId.Create(parsedUpdateOwnerId));

        if (updateOwner is null)
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        if (!Guid.TryParse(request.ArticleId, out Guid parsedArticleId))
        {
            return Errors.Article.InvalidArticle;
        }

        var article = await _articleRepo.GetArticleByIdAsync(ArticleId.Create(parsedArticleId));

        if (article is null)
        {
            return Errors.Article.InvalidArticle;
        }

        var update = ArticleUpdate.Create(
            request.Title,
            request.Content,
            request.Reference
        );

        update.AddUpdateOwner(updateOwner);

        article.UpdateArticle(update);

        await _articleRepo.UpdateArticleAsync(article);

        return new ArticleResult(
            article,
            update
        );

    }
}
