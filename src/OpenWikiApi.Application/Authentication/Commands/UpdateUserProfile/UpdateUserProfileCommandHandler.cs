using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.Entities.UserProfiles;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Authentication.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, ErrorOr<Unit>>
{
    private readonly IAuthenticationRepository _authRepo;

    public UpdateUserProfileCommandHandler(
        IAuthenticationRepository authRepo
    )
    {
        _authRepo = authRepo;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
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

        var profile = UserProfile.Create(
            request.Name,
            request.Age,
            request.Email
        );

        user.UpdateUserProfile(profile);

        await _authRepo.UpdateUserAsync(user);

        return Unit.Value;
    }
}
