using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Constants;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Authentication.Commands.AddUserRole.Admin;

public class AddUserAdminRoleCommmandHandler : IRequestHandler<AddUserAdminRoleCommmand, ErrorOr<Unit>>
{
    private readonly IAuthenticationRepository _authRepo;

    public AddUserAdminRoleCommmandHandler(
        IAuthenticationRepository authRepo
    )
    {
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<Unit>> Handle(AddUserAdminRoleCommmand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out Guid parsedUserId))
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var user = await _authRepo.GetUserByIdWithRolesAndPermissions(UserId.Create(parsedUserId));

        if (user is null)
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var IsUserAnAdmin = user.Roles.Any(role =>
            role.Name == Role.Admin.ToString()
        );

        if (IsUserAnAdmin)
        {
            return Errors.Authentication.UserIsAlreadyAdmin;
        }

        var adminRole = await _authRepo.GetAdminRolesWithPermissions();

        user.AddRole(adminRole);

        await _authRepo.UpdateUserAsync(user);

        return Unit.Value;
    }
}
