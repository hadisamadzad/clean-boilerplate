using System;
using System.Threading.Tasks;

namespace Common.Persistence.DistributedCache;

public interface ICache
{
    Task<T> GetAsync<T>(string key, bool hasAbsoluteKey = false);
    Task<bool> SetAsync<T>(string key, T value);
    Task<bool> SetAsync<T>(string key, T value, TimeSpan ttl);
    Task<bool> RemoveAsync(string key);
}
