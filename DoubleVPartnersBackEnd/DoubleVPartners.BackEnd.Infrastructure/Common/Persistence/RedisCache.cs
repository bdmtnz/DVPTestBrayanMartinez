using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using ErrorOr;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DoubleVPartners.BackEnd.Infrastructure.Common.Persistence
{
    public class RedisCache(IDistributedCache _cache) : ICache
    {
        public async Task<ErrorOr<T>> Get<T>(string key)
        {
            try
            {
                var cachedData = await _cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(cachedData))
                {
                    return Error.Failure(description: "Cache miss");
                }

                var jsonObj = JsonSerializer.Deserialize<T>(cachedData);
                if (jsonObj is null)
                {
                    return Error.Failure(description: "Cache miss");
                }

                return jsonObj;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }

        public async Task Set<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(60),
                SlidingExpiration = unusedExpireTime ?? TimeSpan.FromMinutes(20)
            };
            var jsonData = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, jsonData, options);
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
