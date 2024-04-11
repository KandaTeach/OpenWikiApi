using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;

namespace OpenWikiApi.Application.Authentication.Queries.LoginUser;

public record LoginUserQuery(
    string Username,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;