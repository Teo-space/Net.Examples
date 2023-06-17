using Microsoft.Extensions.Caching.Memory;

namespace Net.Examles.Examples.MemoryCaches;


public class MemoryCacheWithPolicy<TItem>
{
    private MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions()
    {
        SizeLimit = 1024
    });

    public TItem GetOrCreate(object key, Func<TItem> createItem)
    {
        if (!memoryCache.TryGetValue(key, out TItem cacheEntry))
        {
            cacheEntry = createItem();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSize(1)
            .SetPriority(CacheItemPriority.High)
            .SetSlidingExpiration(TimeSpan.FromSeconds(2))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

            memoryCache.Set(key, cacheEntry, cacheEntryOptions);
        }
        return cacheEntry;
    }



}