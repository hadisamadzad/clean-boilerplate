namespace Blog.Application.Types.Entities;

public class TagEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string Slug { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}