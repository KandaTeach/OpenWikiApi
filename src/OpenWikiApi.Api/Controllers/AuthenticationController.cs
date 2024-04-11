using System.Security.Claims;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OpenWikiApi.Application.Authentication.Commands.AddUserRole.Admin;
using OpenWikiApi.Application.Authentication.Commands.AddUserRole.Member;
using OpenWikiApi.Application.Authentication.Commands.RegisterUser.Admin;
using OpenWikiApi.Application.Authentication.Commands.RegisterUser.Member;
using OpenWikiApi.Application.Authentication.Commands.UpdateUserCredential;
using OpenWikiApi.Application.Authentication.Commands.UpdateUserProfile;
using OpenWikiApi.Application.Authentication.Queries.LoginUser;
using OpenWikiApi.Application.Authentication.Queries.UserDashboard;
using OpenWikiApi.Contract.Authentication;
using OpenWikiApi.Domain.Common.Constants;
using OpenWikiApi.Infrastructure.Authorization.Jwt;

namespace OpenWikiApi.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// View dashboard of this user.
    /// </summary>
    /// <returns>The dashboard of this user.</returns>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Delete)]
    [HttpGet("user/dashboard")]
    public async Task<IActionResult> UserDashboardAsync()
    {
        var query = new UserDashboardQuery(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var result = await _mediator.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<UserDashboardResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Login the user.
    /// </summary>
    /// <param name="request">Provide the username and password.</param>
    /// <returns>The user information and its token.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest request)
    {
        var query = _mapper.Map<LoginUserQuery>(request);

        var result = await _mediator.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<LoginResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Register a member role user.
    /// </summary>
    /// <param name="request">Provide the nickname, profile, and credential of the user.</param>
    /// <returns>The successfully registered member role user.</returns>
    [AllowAnonymous]
    [HttpPost("register/member")]
    public async Task<IActionResult> RegisterMemberAsync([FromBody] RegisterRequest request)
    {
        var command = _mapper.Map<RegisterMemberCommand>(request);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<RegisterResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Register an admin role user.
    /// </summary>
    /// <param name="request">Provide the nickname, profile, and credential of the user.</param>
    /// <returns>The successfully registered admin role user.</returns>
    [AllowAnonymous]
    [HttpPost("register/admin")]
    public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterRequest request)
    {
        var command = _mapper.Map<RegisterAdminCommand>(request);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<RegisterResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Adds an admin role to an existed user.
    /// </summary>
    /// <param name="userId">Provide the id of the user.</param>
    [AuthorizeUserByJwt(new Role[] { Role.Admin }, Permission.Delete)]
    [HttpPost("user/add/role/admin")]
    public async Task<IActionResult> AddUserAdminRoleAsync([FromQuery] string userId)
    {
        var command = new AddUserAdminRoleCommmand(userId);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Adds a member role to an existed user.
    /// </summary>
    /// <param name="userId">Provide the id of the user.</param>
    [AuthorizeUserByJwt(new Role[] { Role.Admin }, Permission.Delete)]
    [HttpPost("user/add/member/admin")]
    public async Task<IActionResult> AddUserMemberRoleAsync([FromQuery] string userId)
    {
        var command = new AddUserMemberRoleCommand(userId);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Updates a user's credentials
    /// </summary>
    /// <param name="request">Provide the updated username and password of the user.</param>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Delete)]
    [HttpPost("user/update/credential")]
    public async Task<IActionResult> UpdateUserCredentialAsync([FromBody] UpdateUserCredentialRequest request)
    {
        var partialCommand = _mapper.Map<UpdateUserCredentialCommand>(request);
        var command = partialCommand.MapUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Updates a user's profile.
    /// </summary>
    /// <param name="request">Provide the updated profile of the user.</param>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Delete)]
    [HttpPost("user/update/profile")]
    public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateUserProfileRequest request)
    {
        var partialCommand = _mapper.Map<UpdateUserProfileCommand>(request);
        var command = partialCommand.MapUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(),
            errors => Problem(errors)
        );
    }

}