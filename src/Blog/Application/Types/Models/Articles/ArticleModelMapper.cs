using Blog.Application.Types.Entities;

namespace Blog.Application.Types.Models.Articles;

public static class ArticleModelMapper
{
    public static ArticleModel MapToModel(this ArticleEntity entity)
    {
        return new ArticleModel
        {
            Id = entity.Id,
            AuthorId = entity.AuthorId,

            Title = entity.Title,
            Subtitle = entity.Subtitle,
            Summary = entity.Summary,
            Content = entity.Content,
            Slug = entity.Slug,
            ThumbnailUrl = entity.ThumbnailUrl,
            CoverImageUrl = entity.CoverImageUrl,

            TimeToReadInMinute = entity.TimeToReadInMinute,
            Likes = entity.Likes,

            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            PublishedAt = entity.PublishedAt,
            ArchivedAt = entity.ArchivedAt,
        };
    }

    public static IEnumerable<ArticleModel> MapToModels(this IEnumerable<ArticleEntity> entities)
    {
        foreach (var entity in entities)
            yield return entity.MapToModel();
    }
}
