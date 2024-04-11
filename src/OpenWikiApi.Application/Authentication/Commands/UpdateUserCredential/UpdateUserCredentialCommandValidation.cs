using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Commands.UpdateUserCredential;

public class UpdateUserCredentialCommandValidation : AbstractValidator<UpdateUserCredentialCommand>
{
    public UpdateUserCredentialCommandValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Username)
            .MaximumLength(15)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Password)
            .MaximumLength(15)
            .NotEmpty()
            .NotNull();
    }
}