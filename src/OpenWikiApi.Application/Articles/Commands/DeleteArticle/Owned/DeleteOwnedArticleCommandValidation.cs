using FluentValidation;

namespace OpenWikiApi.Application.Articles.Commands.DeleteArticle.Owned;

public class DeleteOwnedArticleCommandValidation : AbstractValidator<DeleteOwnedArticleCommand>
{
    public DeleteOwnedArticleCommandValidation()
    {
        RuleFor(x => x.ArticleId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .NotNull();
    }
}