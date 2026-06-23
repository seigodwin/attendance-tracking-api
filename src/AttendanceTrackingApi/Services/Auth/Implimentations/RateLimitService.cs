

using AttendanceTrackingApi.Services.Auth.Interface;
using StackExchange.Redis;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
	public class RateLimitService : IRateLimitService
	{
        private readonly IDatabase _redis;
        public RateLimitService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase(); 
        }

        public async Task<bool> IsRateLimited(string key , int limit , TimeSpan window)
        {
            long attempt = 0;

            if (!string.IsNullOrEmpty(key))
            {
                attempt = await _redis.StringIncrementAsync(key); 
            }

            if(attempt == 1)                            
            {
                await _redis.KeyExpireAsync(key, window); 
            } 

            return attempt > limit ? true : false; 
        }
    }
}
