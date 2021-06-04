using System;
using System.Threading.Tasks;
using Identity.Application.Interfaces.Repositories;

namespace Identity.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        Task<bool> CommitAsync();
    }
}