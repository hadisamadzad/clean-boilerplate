using Blog.Application.Interfaces.Repositories;

namespace Blog.Application.Interfaces;

public interface IRepositoryManager
{
    IArticleRepository Articles { get; }
    ITagRepository Tags { get; }
    ISubscriberRepository Subscribers { get; }
    ISettingRepository Settings { get; }

    Task<bool> CommitAsync();
}
