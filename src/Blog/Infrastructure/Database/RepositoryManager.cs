using Blog.Application.Interfaces;
using Blog.Application.Interfaces.Repositories;
using Blog.Infrastructure.Database.Repositories;
using MongoDB.Driver;

namespace Blog.Infrastructure.Database;

public class RepositoryManager(IMongoDatabase mongoDatabase) : IRepositoryManager
{
    public IArticleRepository Articles { get; } =
        new ArticleRepository(mongoDatabase, "blog.articles");

    public ITagRepository Tags { get; } =
        new TagRepository(mongoDatabase, "blog.tags");

    public ISettingRepository Settings { get; } =
        new SettingRepository(mongoDatabase, "blog.settings");

    public async Task<bool> CommitAsync()
    {
        var task = new Task<bool>(() => { return true; });
        return await task;
    }
}
