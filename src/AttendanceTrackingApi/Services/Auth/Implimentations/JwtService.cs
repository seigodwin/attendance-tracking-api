
using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Services.Auth.Interface;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
    public class JwtService : IJwtService
    {
        public Task<AuthenticatedUserDto> GenerateTokenPairAsync(Admin model)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAccessTokenAsync(RefreshAccessTokenRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}