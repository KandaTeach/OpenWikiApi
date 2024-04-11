using FluentValidation;

namespace OpenWikiApi.Application.Articles.Queries.ReadArticleList.Owned;

public class ReadOwnedArticleListQueryValidation : AbstractValidator<ReadOwnedArticleListQuery>
{
    public ReadOwnedArticleListQueryValidation()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .NotNull();
    }
}