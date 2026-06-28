using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class ForgotPasswordResponseDto
    {
        [Required]
        public required string ResetPasswordToken {get ; set;} 
    }
}
