using System;
using Microsoft.Extensions.Caching.Memory;

namespace Blipkart.Core.Infra
{
    public class CacheHelper<T> : ICacheHelper<T>
    {
        private readonly IMemoryCache _memoryCache;

        public CacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool TryGet(string key, out T item)
        {
            return _memoryCache.TryGetValue(key, out item);
        }

        public void Set(string key, T item)
        {
            ICacheEntry entry = _memoryCache.CreateEntry(key);
            entry.Value = item;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
