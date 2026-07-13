

using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Services.Auth.Interface;
using AttendanceTrackingApi.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Admin> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IDatabase _redis;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<Admin> userManager, IConnectionMultiplexer redis, ITokenService tokenService, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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

        public async Task<BaseResponse<string>> ChangePasswordAsync(ChangePasswordRequestDto dto)
        {
            var response = new BaseResponse<string>();

            try
            {
                if (dto is null)
                {
                    response.Success = false;
                    response.Message = "Provide valid data to continue";
                    return response;
                }

                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    response.Success = false;
                    response.Message = "Passwords do not match";
                    return response;
                }

                var user = await _userManager.FindByEmailAsync(dto.EmailAddress);

                if (user is null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }

                var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = string.Join("; ", result.Errors.Select(e => e.Description));
                    return response;
                }

                response.Message = "Password changed successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing password for email {Email}. Exception chain: {ExceptionDetails}", dto?.EmailAddress, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to change password. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<ForgotPasswordResponseDto>> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            var response = new BaseResponse<ForgotPasswordResponseDto>();

            try
            {
                if (dto is null)
                {
                    response.Success = false;
                    response.Message = "Provide valid data to continue";
                    return response;
                }

                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user is null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var redisKey = $"password-reset:{resetToken}";

                await _redis.StringSetAsync(redisKey, user.Id, TimeSpan.FromMinutes(15));

                response.Data = new ForgotPasswordResponseDto
                {
                    ResetPasswordToken = resetToken
                };
                response.Message = "Password reset token generated successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing forgot password for email {Email}. Exception chain: {ExceptionDetails}", dto?.Email, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to process forgot password request. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<AuthenticatedUserDto>> LoginAsync(LoginRequestDto dto)
        {
            var response = new BaseResponse<AuthenticatedUserDto>();

            try
            {
                if (dto is null)
                {
                    response.Success = false;
                    response.Message = "Provide valid data to continue";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                {
                    response.Success = false;
                    response.Message = "Invalid username or password";
                    return response;
                }

                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user is null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }

                var isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

                if (!isValidPassword)
                {
                    response.Success = false;
                    response.Message = "Incorrect password";
                    return response;
                }

                response.Data = await _tokenService.GenerateTokenPairAsync(user);
                response.Message = "Login successful";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user {UserName}. Exception chain: {ExceptionDetails}", dto?.Email, BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Login failed. Please try again later.";
            }

            return response;
        }

        public async Task<BaseResponse<string>> ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            var response = new BaseResponse<string>();

            try
            {
                if (dto is null)
                {
                    response.Success = false;
                    response.Message = "Provide valid data to continue";
                    return response;
                }

                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    response.Success = false;
                    response.Message = "Passwords do not match";
                    return response;
                }

                var redisKey = $"password-reset:{dto.ResetPasswordToken}";
                var userId = await _redis.StringGetAsync(redisKey);

                if (userId.IsNullOrEmpty)
                {
                    response.Success = false;
                    response.Message = "Invalid or expired password reset token";
                    return response;
                }

                var user = await _userManager.FindByIdAsync(userId!);

                if (user is null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }

                var result = await _userManager.ResetPasswordAsync(user, dto.ResetPasswordToken, dto.NewPassword);

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = string.Join("; ", result.Errors.Select(e => e.Description));
                    return response;
                }

                await _redis.KeyDeleteAsync(redisKey);

                response.Message = "Password reset successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resetting password. Exception chain: {ExceptionDetails}", BuildExceptionDetails(ex));
                response.Success = false;
                response.Message = "Failed to reset password. Please try again later.";
            }

            return response;
        }
    }
}
