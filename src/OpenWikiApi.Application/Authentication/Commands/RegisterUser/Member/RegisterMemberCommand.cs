using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;

namespace OpenWikiApi.Application.Authentication.Commands.RegisterUser.Member;

public record RegisterMemberCommand(
    string Nickname,
    RegisterMemberCredential Credential,
    RegisterMemberProfile Profile
) : IRequest<ErrorOr<AuthenticationResult>>;

public record RegisterMemberCredential(
    string Username,
    string Password
);

public record RegisterMemberProfile(
    string Name,
    int Age,
    string Email
);