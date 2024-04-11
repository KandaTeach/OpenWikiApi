using System.Security.Claims;

using MapsterMapper;
using MediatR;

using Microsoft.AspNetCore.Mvc;

using OpenWikiApi.Application.Articles.Commands.DeleteArticle.Any;
using OpenWikiApi.Application.Articles.Commands.DeleteArticle.Owned;
using OpenWikiApi.Application.Articles.Commands.InsertArticle;
using OpenWikiApi.Application.Articles.Commands.UpdateArticle;
using OpenWikiApi.Application.Articles.Queries.ReadArticle;
using OpenWikiApi.Application.Articles.Queries.ReadArticleList.Owned;
using OpenWikiApi.Application.Articles.Queries.ReadArticleUpdateList;
using OpenWikiApi.Contract.Articles;
using OpenWikiApi.Domain.Common.Constants;
using OpenWikiApi.Infrastructure.Authorization.Jwt;

namespace OpenWikiApi.Api.Controllers;

[Route("article")]
public class ArticleController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ArticleController(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// View the article by providing the id of the article.
    /// </summary>
    /// <param name="articleId">Provide the id of the article.</param>
    /// <returns>The article.</returns>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Read)]
    [HttpGet("view")]
    public async Task<IActionResult> ViewArticleAsync([FromQuery] string articleId)
    {
        var query = new ReadArticleQuery(articleId);

        var result = await _mediator.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<ReadArticleResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// View the list of owned articles.
    /// </summary>
    /// <returns>The list of owned articles.</returns>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Read)]
    [HttpGet("view/all/owned")]
    public async Task<IActionResult> ViewOwnedArticlesAsync()
    {
        var query = new ReadOwnedArticleListQuery(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var result = await _mediator.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<ReadListOfArticlesResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// View the list of updates from the article.
    /// </summary>
    /// <param name="articleId">Provide the id of the article.</param>
    /// <returns>The list of updates from the article</returns>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Read)]
    [HttpGet("view/update")]
    public async Task<IActionResult> ViewUpdatesFromArticleAsync([FromQuery] string articleId)
    {
        var query = new ReadArticleUpdateListQuery(articleId);

        var result = await _mediator.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<ReadListOfArticleUpdatesResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Adds an article.
    /// </summary>
    /// <param name="request">Provide the article's title, content, and reference.</param>
    /// <returns>The successfully added article.</returns>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Insert)]
    [HttpPost("new")]
    public async Task<IActionResult> AddNewArticleAsync([FromBody] AddArticleRequest request)
    {
        var partialCommand = _mapper.Map<InsertArticleCommand>(request);
        var command = partialCommand.MapOwnerId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<AddArticleResponse>(value)),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Adds an update to the article.
    /// </summary>
    /// <param name="request">Provide the id of the article and its updated title, content, and reference.</param>
    /// <returns>The successfully added update of the article.</returns>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Delete)]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateArticleAsync([FromBody] UpdateArticleRequest request)
    {
        var partialCommand = _mapper.Map<UpdateArticleCommand>(request);
        var command = partialCommand.MapUpdateOwnerId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<UpdateArticleResponse>(value)),
            errors => Problem(errors)
        );

    }

    /// <summary>
    /// Deletes an owned article.
    /// </summary>
    /// <param name="articleId">Provide the id of the article.</param>
    [AuthorizeUserByJwt(new Role[] { Role.Member, Role.Admin }, Permission.Delete)]
    [HttpDelete("delete/owned")]
    public async Task<IActionResult> DeleteOwnedArticleAsync([FromQuery] string articleId)
    {
        var command = new DeleteOwnedArticleCommand(
            articleId,
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(),
            errors => Problem(errors)
        );
    }

    /// <summary>
    /// Deletes any article.
    /// </summary>
    /// <param name="articleId">Provide the id of the article</param>
    [AuthorizeUserByJwt(new Role[] { Role.Admin }, Permission.Delete)]
    [HttpDelete("delete/any")]
    public async Task<IActionResult> DeleteAnyArticleAsync([FromQuery] string articleId)
    {
        var command = new DeleteAnyArticleCommand(articleId);

        var result = await _mediator.Send(command);

        return result.Match(
            value => Ok(),
            errors => Problem(errors)
        );
    }

}