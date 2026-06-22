
namespace AttendanceTrackingApi.Options
{
    public class JwtOptions
    {
        public string SECRET {get ; set;} = string.Empty;
        public string AUDIENCE {get ; set;} = string.Empty;
        public string ISSUER {get ; set;} = string.Empty;
        public int EXPIRATION {get ; set;}
    }
}