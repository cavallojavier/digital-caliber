using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace digital.caliber.services.Cache
{
    public class CustomMemoryCache
    {
        private const int ExpirationTime = 5;
        
        public MemoryCache Cache { get; set; }

        public CustomMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                //SizeLimit = 1024,
            });
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="cachingObject">Item to be cached</param>
        /// <param name="key">Name of item</param>
        /// <param name="expirationInMinute"></param>
        public async Task AddAsync<T>(T cachingObject, string key, int? expirationInMinute = null)
        {
            ClearAsync(key);

            await Task.FromResult(Cache.Set(
                    key,
                    cachingObject,
                    DateTime.Now.AddMinutes(expirationInMinute ?? ExpirationTime)
            ));
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public void ClearAsync(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return await Task.FromResult(Cache.Get(key) != null);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var cacheEntry = await Task.FromResult(Cache.Get(key));

            return (T) cacheEntry;
        }
    }
}
