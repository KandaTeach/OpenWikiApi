using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users;
using OpenWikiApi.Domain.Users.Entities.UserCredentials;
using OpenWikiApi.Domain.Users.Entities.UserProfiles;

namespace OpenWikiApi.Application.Authentication.Commands.RegisterUser.Member;

public class RegisterMemberCommandHandler : IRequestHandler<RegisterMemberCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IAuthenticationRepository _authRepo;

    public RegisterMemberCommandHandler(
        IAuthenticationRepository authRepo
    )
    {
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
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

        var memberRole = await _authRepo.GetMemberRolesWithPermissions();

        user.AddRole(memberRole);

        await _authRepo.CreateUserAsync(user);

        return new AuthenticationResult(
            user,
            null!
        );
    }
}