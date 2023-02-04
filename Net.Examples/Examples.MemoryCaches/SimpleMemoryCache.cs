using Microsoft.Extensions.Caching.Memory;


namespace Net.Examles.Examples.MemoryCaches;


public class SimpleMemoryCache<TItem>
{
    private MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

    public TItem GetOrCreate(object key, Func<TItem> createItem)
    {
        if (!memoryCache.TryGetValue(key, out TItem cacheEntry))
        {
            cacheEntry = createItem();
            memoryCache.Set(key, cacheEntry);
        }
        return cacheEntry;
    }


}

