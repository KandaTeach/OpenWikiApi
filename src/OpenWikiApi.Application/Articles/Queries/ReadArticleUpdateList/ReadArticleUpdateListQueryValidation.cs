using FluentValidation;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticleUpdateList;

public class ReadArticleUpdateListQueryValidation : AbstractValidator<ReadArticleUpdateListQuery>
{
    public ReadArticleUpdateListQueryValidation()
    {
        RuleFor(x => x.ArticleId)
            .NotEmpty()
            .NotNull();
    }
}