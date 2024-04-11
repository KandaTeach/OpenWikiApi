using Mapster;

using OpenWikiApi.Application.Articles.Common;
using OpenWikiApi.Contract.Articles;

namespace OpenWikiApi.Api.Common.Mappings;

public class ArticleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ArticleResult, AddArticleResponse>()
            .Map(des => des, src => src.Article)
            .Map(des => des.ArticleAuthor, src => src.Article.Owner.Nickname);

        config.NewConfig<ArticleResult, UpdateArticleResponse>()
            .Map(des => des.UpdateId, src => src.Update.Id)
            .Map(des => des, src => src.Update)
            .Map(des => des.UpdateOwner, src => src.Update.UpdateOwner.Nickname)
            .Map(des => des.FromThisArticle, src =>
                new ReferenceArticleResponse(
                        src.Article.Id,
                        src.Article.Title
                    )
            )
            .Map(des => des.UpdatedDateTime, src => src.Update.CreatedDateTime);

        config.NewConfig<ArticleResult, ReadArticleResponse>()
            .IgnoreNullValues(true)
            .Map(des => des.Title, src => src.Update != null! ? src.Update.Title : src.Article.Title)
            .Map(des => des.Content, src => src.Update != null! ? src.Update.Content : src.Article.Content)
            .Map(des => des.Reference, src => src.Update != null! ? src.Update.Reference : src.Article.Reference)
            .Map(des => des.UpdateInformation, src => src.Update != null! ?
                    new ViewUpdatedArticleResponse(
                        src.Update.Id,
                        src.Update.UpdateOwner.Nickname,
                        src.Update.CreatedDateTime
                    ) : null)
            .Map(des => des.ArticleSource, src =>
                new ViewArticleSourceResponse(
                    src.Article.Id,
                    src.Article.Owner.Nickname,
                    src.Article.CreatedDateTime
                ))
            .Map(des => des.ListOfUpdates, src => src.Article.Updates.Any() ?
                src.Article.Updates
                    .Select(update => new ViewArticleUpdateResponse(
                        update.Id,
                        update.Title
                    )) : null);

        config.NewConfig<ArticleListResult, ReadListOfArticlesResponse>()
            .Map(des => des.Articles, src => src.Articles
                .Select(article => new ViewArticleResponse(
                    article.Id,
                    article.Updates.Any() ? article.Updates
                        .OrderByDescending(
                            x => x.CreatedDateTime
                        )
                        .FirstOrDefault()!.Title
                    : article.Title
                )));

        config.NewConfig<ArticleListResult, ReadListOfArticleUpdatesResponse>()
            .Map(des => des.ArticleUpdates, src => src.Updates
                .Select(update => new ViewArticleUpdateResponse(
                    update.Id,
                    update.Title
                )));

    }
}