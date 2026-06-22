
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Options;
using AttendanceTrackingApi.Services.Auth.Interface;
using AttendanceTrackingApi.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<Admin> _userManager;
        private readonly IDatabase _redis;

        public TokenService(IOptions<JwtOptions> jwtOptions , UserManager<Admin> userManager
        , IConnectionMultiplexer redis)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _redis = redis.GetDatabase();
        }

        public async Task<AuthenticatedUserDto> GenerateTokenPairAsync(Admin model)
        {
            // Generate access token
            var tokenHandler = new JsonWebTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SECRET);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , model.Id),
                new Claim(JwtRegisteredClaimNames.Email , model.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Name , model.UserName ?? string.Empty)
            };

            var roles = await _userManager.GetRolesAsync(model);

            if(roles is not null)
            {
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), 
                Audience = _jwtOptions.AUDIENCE,
                Issuer = _jwtOptions.ISSUER,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.EXPIRATION)
            }; 

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            //Generate RefreshToken
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            var refreshTokenValue = Convert.ToBase64String(randomBytes);


            string cacheKey = $"refresh-token:{model.Id}";

            await _redis.StringSetAsync(
                cacheKey, refreshTokenValue, TimeSpan.FromDays(7)
            );

            return new AuthenticatedUserDto
            {
                UserName = model.UserName ?? string.Empty,
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiry = _jwtOptions.EXPIRATION 
            };
        } 

        public Task<BaseResponse<string>> RefreshAccessTokenAsync(RefreshAccessTokenRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}