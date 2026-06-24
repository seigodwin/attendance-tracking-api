
using System.Text.Json;
using AttendanceTrackingApi.Services.Caching.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace AttendanceTrackingApi.Services.Caching.Implimentations
{
    public class DistributedCacheService : IDistributedCacheService
    {
        private readonly IDistributedCache _cache;
        public DistributedCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
           if (string.IsNullOrEmpty(key)) return default;  
           
           var data = await _cache.GetStringAsync(key);

           if(string.IsNullOrEmpty(data)) return default;

           return JsonSerializer.Deserialize<T>(data);
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key)) return;
           
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T? value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
        {
           if (string.IsNullOrEmpty(key) || value is null) return;
           
            var options = new DistributedCacheEntryOptions();
            if (slidingExpiration.HasValue)
            {
                options.SlidingExpiration = slidingExpiration;
            }

            if (absoluteExpiration.HasValue) 
            {   
                options.AbsoluteExpirationRelativeToNow = absoluteExpiration; 
            }

            var jsonData = JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(key, jsonData, options);
        }
    }
}