using System.Linq.Expressions;
using Identity.Application.Types.Entities;

namespace Identity.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    // Commands
    void Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
}