using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticleList.Owned;

public class ReadOwnedArticleListQueryHandler : IRequestHandler<ReadOwnedArticleListQuery, ErrorOr<ArticleListResult>>
{
    private readonly IArticleRepository _articleRepo;

    public ReadOwnedArticleListQueryHandler(
        IArticleRepository articleRepo
    )
    {
        _articleRepo = articleRepo;
    }

    public async Task<ErrorOr<ArticleListResult>> Handle(ReadOwnedArticleListQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.OwnerId, out Guid parsedOwnerId))
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var articles = await _articleRepo.GetListOfOwnedArticlesAsync(UserId.Create(parsedOwnerId));

        if (articles?.Count == 0)
        {
            return Errors.Article.NoArticles;
        }

        return new ArticleListResult(
            articles!,
            null!
        );
    }
}
