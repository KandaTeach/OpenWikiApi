using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandValidation : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Age)
            .LessThanOrEqualTo(100)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Email)
            .MaximumLength(100)
            .NotEmpty()
            .NotNull();
    }
}