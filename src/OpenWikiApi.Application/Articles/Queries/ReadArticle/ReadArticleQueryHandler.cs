using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Articles.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Common.Errors;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticle;

public class ReadArticleQueryHandler : IRequestHandler<ReadArticleQuery, ErrorOr<ArticleResult>>
{
    private readonly IArticleRepository _articleRepo;

    public ReadArticleQueryHandler(
        IArticleRepository articleRepo
    )
    {
        _articleRepo = articleRepo;
    }

    public async Task<ErrorOr<ArticleResult>> Handle(ReadArticleQuery request, CancellationToken cancellationToken)
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

        var latestUpdate = article.Updates.OrderByDescending(
                x => x.CreatedDateTime
            )
            .FirstOrDefault();

        return new ArticleResult(
            article,
            latestUpdate!
        );
    }
}