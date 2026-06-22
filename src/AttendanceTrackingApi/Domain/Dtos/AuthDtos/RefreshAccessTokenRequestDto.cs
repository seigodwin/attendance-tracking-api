namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class RefreshAccessTokenRequestDto
    {
        public string UserName {get ; set;} = string.Empty;
        public string AccessToken {get ; set;} = string.Empty;
        public string RefreshToken {get ; set;} = string.Empty;
        public DateTime AccessTokenExpiry {get ; set;}
    }
}