using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Commands.RegisterUser.Member;

public class RegisterMemberCommandValidation : AbstractValidator<RegisterMemberCommand>
{
    public RegisterMemberCommandValidation()
    {
        RuleFor(x => x.Nickname)
            .MaximumLength(15)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Credential)
            .ChildRules(c =>
            {
                c.RuleFor(x => x.Username)
                    .MaximumLength(15);

                c.RuleFor(x => x.Password)
                    .MaximumLength(25);
            })
        .NotEmpty()
        .NotNull();

        RuleFor(x => x.Profile)
            .ChildRules(act =>
            {
                act.RuleFor(x => x.Name)
                    .MaximumLength(100);

                act.RuleFor(x => x.Age)
                    .LessThanOrEqualTo(100);

                act.RuleFor(x => x.Email)
                    .MaximumLength(100);
            })
            .NotEmpty()
            .NotNull();
    }
}