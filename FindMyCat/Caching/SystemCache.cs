using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace FindMyCat.Caching
{
    public class SystemCache : ICache
    {
        const int DEFAULT_CACHE_DURATION_MINS = 15;

        public T Get<T>(string key) where T : class
        {
            object data = MemoryCache.Default.Get(key);
            return data as T;
        }

        public bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public void Set<T>(string key, T value)
        {
            this.Set(key, value, DEFAULT_CACHE_DURATION_MINS);
        }

        public void Set<T>(string key, T value, int cacheDurationInMinutes)
        {
            MemoryCache.Default.Set(key, value, new DateTimeOffset(DateTime.Now.AddMinutes(cacheDurationInMinutes)));
        }
    }
}