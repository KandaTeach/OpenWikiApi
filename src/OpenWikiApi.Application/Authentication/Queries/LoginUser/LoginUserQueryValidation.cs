using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Queries.LoginUser;

public class LoginUserQueryValidation : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidation()
    {
        RuleFor(x => x.Username)
            .MaximumLength(15)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Password)
            .MaximumLength(25)
            .NotEmpty()
            .NotNull();
    }
}