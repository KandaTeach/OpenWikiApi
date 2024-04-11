using ErrorOr;
using MediatR;

using OpenWikiApi.Application.Authentication.Common;
using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Application.Common.Interfaces.Services.Authentication;
using OpenWikiApi.Domain.Common.Errors;

namespace OpenWikiApi.Application.Authentication.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IAuthenticationRepository _authRepo;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginUserQueryHandler(
        IAuthenticationRepository authRepo,
        IJwtTokenGenerator tokenGenerator
    )
    {
        _authRepo = authRepo;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _authRepo.GetUserByCredentialsAsync(request.Username, request.Password);

        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _tokenGenerator.GenerateTokenAsync(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}