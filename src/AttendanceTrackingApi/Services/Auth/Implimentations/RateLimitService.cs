

using AttendanceTrackingApi.Services.Auth.Interface;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
	public class RateLimitService : IRateLimitService
	{
        private readonly IDatabase _redis;
        private readonly ILogger<RateLimitService> _logger;

        public RateLimitService(IConnectionMultiplexer redis, ILogger<RateLimitService> logger)
        {
            _redis = redis.GetDatabase();
            _logger = logger;
        }

        private static string BuildExceptionDetails(Exception exception)
        {
            var details = new List<string>();
            var current = exception;

            while (current is not null)
            {
                details.Add($"{current.GetType().Name}: {current.Message}");
                current = current.InnerException;
            }

            return string.Join(" -> ", details);
        }

        public async Task<bool> IsRateLimited(string key, int limit, TimeSpan window)
        {
            try
            {
                long attempt = 0;

                if (!string.IsNullOrEmpty(key))
                {
                    attempt = await _redis.StringIncrementAsync(key);
                }

                if (attempt == 1)
                {
                    await _redis.KeyExpireAsync(key, window);
                }

                return attempt > limit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while applying rate limiting for key {Key}. Exception chain: {ExceptionDetails}", key, BuildExceptionDetails(ex));
                return false;
            }
        }
    }
}
