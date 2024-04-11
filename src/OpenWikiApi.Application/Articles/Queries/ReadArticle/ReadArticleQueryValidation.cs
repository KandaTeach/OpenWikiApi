using FluentValidation;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticle;

public class ReadArticleQueryValidation : AbstractValidator<ReadArticleQuery>
{
    public ReadArticleQueryValidation()
    {
        RuleFor(x => x.ArticleId)
            .NotEmpty()
            .NotNull();
    }
}