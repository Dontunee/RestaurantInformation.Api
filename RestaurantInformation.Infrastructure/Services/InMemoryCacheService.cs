using Microsoft.Extensions.Caching.Memory;
using RestaurantInformation.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformation.Infrastructure.Services
{
    public class InMemoryCacheService : ICache
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public Task<string> GetItem(string key)
        {
            return  Task.FromResult(_cache.Get<string>(key));
        }


        public Task<bool> AddItem(string key, string data, double time) 
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(time));

            _cache.Set(key, data, cacheEntryOptions);
            return Task.FromResult(true);
        }





    }
}
