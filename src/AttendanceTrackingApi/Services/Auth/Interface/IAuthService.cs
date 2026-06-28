

using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Utilities;

namespace AttendanceTrackingApi.Services.Auth.Interface
{
	public interface IAuthService
    {
        Task<BaseResponse<AuthenticatedUserDto>> LoginAsync(LoginRequestDto dto);
        Task<BaseResponse<ForgotPasswordResponseDto>> ForgotPasswordAsync(ForgotPasswordRequestDto dto);
        Task<BaseResponse<string>> ResetPasswordAsync(ResetPasswordRequestDto dto);
        Task<BaseResponse<string>> ChangePasswordAsync(ChangePasswordRequestDto dto);

    }

}
