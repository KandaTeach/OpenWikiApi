using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Commands.AddUserRole.Admin;

public class AddUserAdminRoleCommmandValidation : AbstractValidator<AddUserAdminRoleCommmand>
{
    public AddUserAdminRoleCommmandValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();
    }
}