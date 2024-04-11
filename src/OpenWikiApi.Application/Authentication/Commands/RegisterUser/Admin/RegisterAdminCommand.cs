using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;

namespace OpenWikiApi.Application.Authentication.Commands.RegisterUser.Admin;

public record RegisterAdminCommand(
    string Nickname,
    RegisterAdminCredential Credential,
    RegisterAdminProfile Profile
) : IRequest<ErrorOr<AuthenticationResult>>;

public record RegisterAdminCredential(
    string Username,
    string Password
);

public record RegisterAdminProfile(
    string Name,
    int Age,
    string Email
);