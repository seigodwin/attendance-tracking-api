
using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Auth.Interface
{
	public interface ITokenService
	{
		Task<AuthenticatedUserDto> GenerateTokenPairAsync(Admin model);
		Task<AuthenticatedUserDto> RefreshAccessTokenAsync(RefreshAccessTokenRequestDto dto);
	}

}