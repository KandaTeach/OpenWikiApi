using FluentValidation;

namespace OpenWikiApi.Application.Articles.Commands.InsertArticle;

public class InsertArticleCommandValidation : AbstractValidator<InsertArticleCommand>
{
    public InsertArticleCommandValidation()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Content)
            .MaximumLength(5000)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Reference)
            .ForEach(r =>
                r.MaximumLength(1000));

        RuleFor(x => x.OwnerId)
            .NotEmpty()
            .NotNull();
    }
}