namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class LoginRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiry { get; set; }
    }
}
