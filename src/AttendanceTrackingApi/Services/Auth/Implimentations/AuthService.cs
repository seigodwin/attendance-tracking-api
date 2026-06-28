

using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Services.Auth.Interface;
using AttendanceTrackingApi.Utilities;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
	public class AuthService : IAuthService
	{
        private readonly UserManager<Admin> _userManager;
        private readonly IDatabase _redis;
        public AuthService(UserManager<Admin> userManager, IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase(); 
            _userManager = userManager;
        }

        public Task<BaseResponse<string>> ChangePasswordAsync(ChangePasswordRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ForgotPasswordResponseDto>> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<AuthenticatedUserDto>> LoginAsync(LoginRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<string>> ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
