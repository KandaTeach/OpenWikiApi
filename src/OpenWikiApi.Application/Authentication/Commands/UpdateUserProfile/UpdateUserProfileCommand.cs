using ErrorOr;
using MediatR;

namespace OpenWikiApi.Application.Authentication.Commands.UpdateUserProfile;

public record UpdateUserProfileCommand(
    string UserId,
    string Name,
    int Age,
    string Email
) : IRequest<ErrorOr<Unit>>
{
    public UpdateUserProfileCommand MapUserId(string userId) =>
        this with { UserId = userId };
};