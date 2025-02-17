using Blog.Application.Types.Entities;
using Common.Interfaces;

namespace Blog.Application.Interfaces.Repositories;

public interface ISubscriberRepository : IRepository<SubscriberEntity>
{
    Task<SubscriberEntity> GetByEmailAsync(string email);
}