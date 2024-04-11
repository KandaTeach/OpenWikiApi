using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Commands.AddUserRole.Member;

public class AddUserMemberRoleCommandValidation : AbstractValidator<AddUserMemberRoleCommand>
{
    public AddUserMemberRoleCommandValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();
    }
}