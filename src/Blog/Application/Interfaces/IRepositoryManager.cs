using Blog.Application.Interfaces.Repositories;

namespace Blog.Application.Interfaces;

public interface IRepositoryManager
{
    IArticleRepository Articles { get; }

    Task<bool> CommitAsync();
}
