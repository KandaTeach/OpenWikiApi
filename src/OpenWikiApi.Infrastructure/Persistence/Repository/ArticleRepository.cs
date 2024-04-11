using Microsoft.EntityFrameworkCore;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Articles.ValueObjects;
using OpenWikiApi.Domain.Users.ValueObjects;
using OpenWikiApi.Infrastructure.Persistence.Context;

namespace OpenWikiApi.Infrastructure.Persistence.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly OpenWikiApiDbContext _context;

    public ArticleRepository(
        OpenWikiApiDbContext context
    )
    {
        _context = context;
    }

    public async Task CreateArticleAsync(Article article)
    {
        await _context.Articles.AddAsync(article);

        await _context.SaveChangesAsync();
    }

    public async Task<Article?> GetUserOwnedArticleByIdAsync(ArticleId articleId, UserId userId)
    {
        var article = await _context.Articles
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == articleId && x.Owner.Id == userId);

        return article;

    }

    public async Task<Article?> GetArticleByIdAsync(ArticleId articleId)
    {
        var article = await _context.Articles
            .Include(x => x.Owner)
            .Include(x => x.Updates)
                .ThenInclude(x => x.UpdateOwner)
            .FirstOrDefaultAsync(x => x.Id == articleId);

        return article;
    }

    public async Task DeleteArticleAsync(Article article)
    {
        _context.Articles.Remove(article);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateArticleAsync(Article article)
    {
        _context.Articles.Update(article);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Article>?> GetListOfOwnedArticlesAsync(UserId userId)
    {
        return await _context.Articles
            .Include(x => x.Owner)
            .Where(x => x.OwnerId == userId)
            .ToListAsync();
    }
}