using Blog.Application.Types.Entities;

namespace Blog.Application.Types.Models.Articles;

public static class ArticleModelMapper
{
    public static ArticleModel MapToModel(this ArticleEntity entity)
    {
        if (entity is null)
            return null;

        return new ArticleModel
        {
            ArticleId = entity.Id,
            State = entity.State,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }

    public static IEnumerable<ArticleModel> MapToModels(this IEnumerable<ArticleEntity> entities)
    {
        foreach (var entity in entities)
            yield return entity.MapToModel();
    }
}
