
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos;
using AttendanceTrackingApi.Options;
using AttendanceTrackingApi.Services.Auth.Interface;
using AttendanceTrackingApi.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace AttendanceTrackingApi.Services.Auth.Implimentations
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<Admin> _userManager;
        private readonly IDatabase _redis;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IOptions<JwtOptions> jwtOptions , UserManager<Admin> userManager
        , IConnectionMultiplexer redis, ILogger<TokenService> logger)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
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

        public async Task<AuthenticatedUserDto> GenerateTokenPairAsync(Admin model)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating token pair for user {UserName}. Exception chain: {ExceptionDetails}", model?.UserName, BuildExceptionDetails(ex));
                throw;
            }
        } 

        public async Task<AuthenticatedUserDto> RefreshAccessTokenAsync(RefreshAccessTokenRequestDto dto)
        {
            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                
                var validTokenParameters = new TokenValidationParameters
                {   ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtOptions.ISSUER,
                    ValidAudience = _jwtOptions.AUDIENCE,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SECRET)),
                };
                
                var validToken = await tokenHandler.ValidateTokenAsync(dto.AccessToken , validTokenParameters);

                if(!validToken.IsValid || validToken.SecurityToken is not JwtSecurityToken token)
                {
                    throw new SecurityException("Invalid access token");
                }

                var userId = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new SecurityException("Invalid user id");
                }
                
                var user = await _userManager.FindByIdAsync(userId);

                if (user is null)
                {
                    throw new SecurityException("User does not exist");
                }

                var cacheKey = $"refresh-token:{userId}";

                var storedRefreshToken = await _redis.StringGetAsync(cacheKey);

                if (storedRefreshToken.IsNullOrEmpty)
                {
                    throw new SecurityException("Invalid refresh token");
                }

                if(storedRefreshToken != dto.RefreshToken)
                {
                    throw new SecurityException("Token mismatch");
                }

                return await GenerateTokenPairAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while refreshing access token. Exception chain: {ExceptionDetails}", BuildExceptionDetails(ex));
                throw;
            }
        }
    }
}