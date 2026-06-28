using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class ForgotPasswordRequestDto
    {
        [EmailAddress]
        [Required]
        public required string Email {get ; set;} 
    }
}
