using ErrorOr;
using MediatR;

namespace OpenWikiApi.Application.Authentication.Commands.AddUserRole.Member;

public record AddUserMemberRoleCommand(
    string UserId
) : IRequest<ErrorOr<Unit>>;