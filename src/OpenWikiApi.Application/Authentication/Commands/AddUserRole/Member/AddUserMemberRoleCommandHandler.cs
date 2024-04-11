using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Authentication.Commands.AddUserRole.Member;

public class AddUserMemberRoleCommandHandler : IRequestHandler<AddUserMemberRoleCommand, ErrorOr<Unit>>
{
    private readonly IAuthenticationRepository _authRepo;

    public AddUserMemberRoleCommandHandler(
        IAuthenticationRepository authRepo
    )
    {
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<Unit>> Handle(AddUserMemberRoleCommand request, CancellationToken cancellationToken)
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

        var IsUserAMember = user.Roles.Any(role =>
            role.Name == Domain.Common.Constants.Role.Member.ToString()
        );

        if (IsUserAMember)
        {
            return Errors.Authentication.UserIsAlreadyMember;
        }

        var memberRole = await _authRepo.GetMemberRolesWithPermissions();

        user.AddRole(memberRole);

        await _authRepo.UpdateUserAsync(user);

        return Unit.Value;
    }
}
