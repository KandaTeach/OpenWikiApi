using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Commands.RegisterUser.Admin;

public class RegisterAdminCommandValidation : AbstractValidator<RegisterAdminCommand>
{
    public RegisterAdminCommandValidation()
    {
        RuleFor(x => x.Nickname)
            .MaximumLength(15)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Credential)
            .ChildRules(c =>
            {
                c.RuleFor(x => x.Username)
                    .MaximumLength(15)
                    .NotEmpty()
                    .NotNull();


                c.RuleFor(x => x.Password)
                    .MaximumLength(25)
                    .NotEmpty()
                    .NotNull();
            })
        .NotEmpty()
        .NotNull();

        RuleFor(x => x.Profile)
            .ChildRules(p =>
            {
                p.RuleFor(x => x.Name)
                    .MaximumLength(100)
                    .NotEmpty()
                    .NotNull();

                p.RuleFor(x => x.Age)
                    .LessThanOrEqualTo(100)
                    .NotEmpty()
                    .NotNull();

                p.RuleFor(x => x.Email)
                    .MaximumLength(100)
                    .NotEmpty()
                    .NotNull();
            })
        .NotEmpty()
        .NotNull();
    }
}