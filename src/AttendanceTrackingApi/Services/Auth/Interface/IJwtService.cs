
using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;

namespace AttendanceTrackingApi.Services.Auth.Interface
{
	public interface IJwtService
	{
		Task<AuthenticatedUserDto> GenerateTokenPairAsync(Admin model);
		Task RefreshAccessTokenAsync(RefreshAccessTokenRequestDto dto);
	}

}