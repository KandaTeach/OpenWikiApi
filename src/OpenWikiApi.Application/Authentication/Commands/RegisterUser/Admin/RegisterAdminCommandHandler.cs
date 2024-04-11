using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users;
using OpenWikiApi.Domain.Users.Entities.UserCredentials;
using OpenWikiApi.Domain.Users.Entities.UserProfiles;

namespace OpenWikiApi.Application.Authentication.Commands.RegisterUser.Admin;

public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IAuthenticationRepository _authRepo;

    public RegisterAdminCommandHandler(
        IAuthenticationRepository authRepo
    )
    {
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
    {
        var IsUsernameExist = await _authRepo.CheckUsernameAsync(request.Credential.Username);

        if (IsUsernameExist)
        {
            return Errors.Authentication.UsernameAlreadyInUse;
        }

        var userCredential = UserCredential.Create(
            request.Credential.Username,
            request.Credential.Password
        );

        var userProfile = UserProfile.Create(
            request.Profile.Name,
            request.Profile.Age,
            request.Profile.Email
        );

        var user = User.Create(
            request.Nickname,
            userCredential,
            userProfile
        );

        var adminRole = await _authRepo.GetAdminRolesWithPermissions();

        user.AddRole(adminRole);

        await _authRepo.CreateUserAsync(user);

        return new AuthenticationResult(
            user,
            null!
        );
    }
}