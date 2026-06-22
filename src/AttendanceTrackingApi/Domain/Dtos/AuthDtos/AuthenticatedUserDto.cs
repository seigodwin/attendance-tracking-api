namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class AuthenticatedUserDto
    {
        public string UserName {get ; set;} = string.Empty;
        public string AccessToken {get ; set;} = string.Empty;
        public string RefreshToken {get ; set;} = string.Empty;
        public int AccessTokenExpiry {get ; set;}
    }
}
