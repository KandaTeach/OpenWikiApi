using ErrorOr;
using MediatR;

namespace OpenWikiApi.Application.Authentication.Commands.UpdateUserCredential;

public record UpdateUserCredentialCommand(
    string UserId,
    string Username,
    string Password
) : IRequest<ErrorOr<Unit>>
{
    public UpdateUserCredentialCommand MapUserId(string userId) =>
        this with { UserId = userId };
};