

using AttendanceTrackingApi.Services.Auth.Interface;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
	public class RateLimitService : IRateLimitService
	{

        public async Task<bool> IsRateLimited(string key)
        {
            return false;
        }
    }
}
