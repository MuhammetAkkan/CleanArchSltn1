using Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Caching;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    public Task<T?> GetAsync<T>(string key)
    {
        if (memoryCache.TryGetValue(key, out T cacheItem)) 
            return Task.FromResult(cacheItem);

        return Task.FromResult(default(T));
    }


    public Task AddAsync<T>(string key, T value, TimeSpan exprTimeSpan)
    {
        //1=> expire time alınacak

        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = exprTimeSpan
        };

        memoryCache.Set(key, value, cacheOptions);

        return Task.CompletedTask; //asenkron bir metot içerisinde senkron bir metot kod yazdığımızdan dolayı Task.CompletedTask kullanarak metotu bitiriyoruz.
    }

    public async Task RemoveAsync(string key)
    {
        throw new NotImplementedException();
    }
}