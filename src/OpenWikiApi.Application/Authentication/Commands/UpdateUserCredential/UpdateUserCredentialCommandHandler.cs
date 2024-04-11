using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.Entities.UserCredentials;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Authentication.Commands.UpdateUserCredential;

public class UpdateUserCredentialCommandHandler : IRequestHandler<UpdateUserCredentialCommand, ErrorOr<Unit>>
{
    private readonly IAuthenticationRepository _authRepo;

    public UpdateUserCredentialCommandHandler(
        IAuthenticationRepository authRepo
    )
    {
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateUserCredentialCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out Guid parsedUserId))
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var user = await _authRepo.GetUserById(UserId.Create(parsedUserId));

        if (user is null)
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var IsUsernameExist = await _authRepo.CheckUsernameAsync(request.Username);

        if (IsUsernameExist)
        {
            return Errors.Authentication.UsernameAlreadyInUse;
        }

        var credential = UserCredential.Create(
            request.Username,
            request.Password
        );

        user.UpdateUserCredential(credential);

        await _authRepo.UpdateUserAsync(user);

        return Unit.Value;

    }
}
