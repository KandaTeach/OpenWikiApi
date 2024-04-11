using FluentValidation;

namespace OpenWikiApi.Application.Articles.Commands.DeleteArticle.Any;

public class DeleteAnyArticleCommandValidation : AbstractValidator<DeleteAnyArticleCommand>
{
    public DeleteAnyArticleCommandValidation()
    {
        RuleFor(x => x.ArticleId)
            .NotEmpty()
            .NotNull();
    }
}