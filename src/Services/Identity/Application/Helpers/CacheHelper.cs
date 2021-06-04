using System;
using Microsoft.Extensions.Caching.Distributed;

namespace Identity.Application.Helpers
{
    public static class CacheHelper
    {
        public static DistributedCacheEntryOptions TtlFromSeconds(int value)
            => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(value)
            };

        public static DistributedCacheEntryOptions TtlFromMinutes(int value)
            => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(value)
            };

        public static DistributedCacheEntryOptions TtlFromHours(int value)
            => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(value)
            };

        public static DistributedCacheEntryOptions TtlFromDays(int value)
            => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(value)
            };
    }
}