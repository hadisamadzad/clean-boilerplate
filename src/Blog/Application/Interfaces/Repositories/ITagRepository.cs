using Blog.Application.Types.Entities;
using Common.Interfaces;

namespace Blog.Application.Interfaces.Repositories;

public interface ITagRepository : IRepository<TagEntity>
{
    Task<TagEntity> GetTagByIdAsync(string id);
    Task<TagEntity> GetTagBySlugAsync(string slug);
}