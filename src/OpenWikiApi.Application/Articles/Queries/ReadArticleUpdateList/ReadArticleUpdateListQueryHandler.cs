using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Common.Errors;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticleUpdateList;

public class ReadArticleUpdateListQueryHandler : IRequestHandler<ReadArticleUpdateListQuery, ErrorOr<ArticleListResult>>
{
    private readonly IArticleRepository _articleRepo;

    public ReadArticleUpdateListQueryHandler(
        IArticleRepository articleRepo
    )
    {
        _articleRepo = articleRepo;
    }

    public async Task<ErrorOr<ArticleListResult>> Handle(ReadArticleUpdateListQuery request, CancellationToken cancellationToken)
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

        var updates = article.Updates.ToList();

        if (updates.Count == 0)
        {
            return Errors.Article.NoUpdates;
        }

        return new ArticleListResult(
            null!,
            updates
        );

    }
}
