using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;

namespace OpenWikiApi.Application.Authentication.Queries.UserDashboard;

public record UserDashboardQuery(
    string UserId
) : IRequest<ErrorOr<AuthenticationResult>>;