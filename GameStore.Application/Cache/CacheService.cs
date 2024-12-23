using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Cache
{
    public class CacheService : ICacheService
    {
        IDistributedCache _cache;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task RemoveCache(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task RemoveCache(string key, int id)
        {
            var cacheKey = $"{key}-{id}";
            await _cache.RemoveAsync(cacheKey);
        }
    }
}
