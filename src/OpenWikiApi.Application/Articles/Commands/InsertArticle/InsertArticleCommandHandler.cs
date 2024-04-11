using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Articles.Commands.InsertArticle;

public class InsertArticleCommandHandler : IRequestHandler<InsertArticleCommand, ErrorOr<ArticleResult>>
{
    private readonly IArticleRepository _articleRepo;
    private readonly IAuthenticationRepository _authRepo;

    public InsertArticleCommandHandler(
        IArticleRepository articleRepo,
        IAuthenticationRepository authRepo
    )
    {
        _articleRepo = articleRepo;
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<ArticleResult>> Handle(InsertArticleCommand request, CancellationToken cancellationToken)
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

        var article = Article.Create(
            request.Title,
            request.Content,
            request.Reference
        );

        article.AddOwner(owner);

        await _articleRepo.CreateArticleAsync(article);

        return new ArticleResult(
            article,
            null!
        );
    }
}