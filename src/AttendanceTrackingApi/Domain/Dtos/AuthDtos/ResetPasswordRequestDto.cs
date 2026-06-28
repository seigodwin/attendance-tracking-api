using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class ResetPasswordRequestDto
    {
        [Required]
        public required string ResetPasswordToken {get ; set;} 
        [DataType(DataType.Password)]
        [Required]
        public required string NewPassword {get ; set;}
        [DataType(DataType.Password)]
        [Required]
        [Compare("NewPassword")]
        public required string ConfirmNewPassword {get ; set;}
        

    }
}
