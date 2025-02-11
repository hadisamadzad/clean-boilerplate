using Common.Interfaces;

namespace Blog.Application.Types.Entities;

public class ArticleEntity : IDeletableEntity
{
    public string Id { get; set; }

    public string Title { get; set; }
    public ArticleState State { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public bool IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; }
}