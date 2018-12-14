using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace digital.caliber.services.Cache
{
    public static class CacheHelper
    {
        private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions() { });
        private const int ExpirationTime = 120;

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="cachingObject">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(T cachingObject, string key)
        {
            Clear(key);
            
            Cache.Set(
                key,
                cachingObject,
                DateTime.Now.AddMinutes(ExpirationTime));
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Clear(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return Cache.Get(key) != null;
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <param name="value">Cached value. Default(T) if item doesn't exist.</param>
        /// <returns>Cached item as type</returns>
        public static bool Get<T>(string key, out T value)
        {
            try
            {
                if (!Exists(key))
                {
                    value = default(T);
                    return false;
                }

                value = (T)Cache.Get(key);
            }
            catch
            {
                value = default(T);
                return false;
            }

            return true;
        }

        public static async Task<T> Get<T>(string key)
        {
            var cacheEntry = await Task.FromResult(Cache.Get(key));
            return (T)cacheEntry;
        }
    }
}
