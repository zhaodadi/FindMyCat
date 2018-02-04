using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyCat.Caching
{
    public interface ICache
    {
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, int cacheDurationInMinutes);
        bool Contains(string key);
        T Get<T>(string key) where T : class;
    }
}