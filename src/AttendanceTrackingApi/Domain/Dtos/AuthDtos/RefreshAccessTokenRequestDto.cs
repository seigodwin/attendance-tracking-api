namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class RefreshAccessTokenRequestDto
    {
        public string AccessToken {get ; set;} = string.Empty;
        public string RefreshToken {get ; set;} = string.Empty;
    }
}