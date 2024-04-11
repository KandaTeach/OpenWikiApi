using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;

public interface IArticleRepository
{
    Task CreateArticleAsync(Article article);
    Task DeleteArticleAsync(Article article);
    Task UpdateArticleAsync(Article article);
    Task<Article?> GetUserOwnedArticleByIdAsync(ArticleId articleId, UserId userId);
    Task<Article?> GetArticleByIdAsync(ArticleId articleId);
    Task<List<Article>?> GetListOfOwnedArticlesAsync(UserId userId);
}