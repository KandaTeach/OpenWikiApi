using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Common.Errors;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Authentication.Queries.UserDashboard;

public class UserDashboardQueryHandler : IRequestHandler<UserDashboardQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IAuthenticationRepository _authRepo;
    private readonly IArticleRepository _articleRepo;

    public UserDashboardQueryHandler(
        IAuthenticationRepository authRepo,
        IArticleRepository articleRepo
    )
    {
        _authRepo = authRepo;
        _articleRepo = articleRepo;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(UserDashboardQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out Guid parsedUserId))
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        var user = await _authRepo.GetUserByIdWithRolesAndArtilcles(UserId.Create(parsedUserId));

        if (user is null)
        {
            return Errors.Authentication.InvalidUserIdentity;
        }

        return new AuthenticationResult(
            user,
            null!
        );
    }
}
