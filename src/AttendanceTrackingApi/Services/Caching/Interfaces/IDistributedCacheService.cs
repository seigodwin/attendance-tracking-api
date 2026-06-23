
namespace AttendanceTrackingApi.Services.Caching.Interfaces
{
    public interface IDistributedCacheService
    {
        Task SetAsync<T>(string key, T? value, TimeSpan? slidingExpiration = null,
        TimeSpan? absoluteExpiration = null);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync (string key);
    }
}