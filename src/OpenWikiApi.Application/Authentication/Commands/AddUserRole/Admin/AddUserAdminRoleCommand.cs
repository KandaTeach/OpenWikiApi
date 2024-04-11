using ErrorOr;
using MediatR;

namespace OpenWikiApi.Application.Authentication.Commands.AddUserRole.Admin;

public record AddUserAdminRoleCommmand(
    string UserId
) : IRequest<ErrorOr<Unit>>;