using Blog.Application.Types.Entities;
using Common.Interfaces;

namespace Blog.Application.Interfaces.Repositories;

public interface ISettingRepository : IRepository<SettingEntity>
{
    Task<SettingEntity> GetBlogSettingAsync();
}