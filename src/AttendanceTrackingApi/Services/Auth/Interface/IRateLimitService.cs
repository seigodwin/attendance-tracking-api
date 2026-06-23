
namespace AttendanceTrackingApi.Services.Auth.Interface
{
	public interface IRateLimitService
	{
		Task<bool> IsRateLimited (string key, int limit, TimeSpan window);
	}

}
