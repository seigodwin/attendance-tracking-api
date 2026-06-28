using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingApi.Dtos.Domain.Dtos.AuthDtos
{
    public class ChangePasswordRequestDto
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public required string EmailAddress {get ; set;}
        [DataType(DataType.Password)]
        [Required]
        public required string CurrentPassword {get ; set;}
        [DataType(DataType.Password)]
        [Required]
        public required string NewPassword {get ; set;}
        [DataType(DataType.Password)]
        [Required]
        [Compare("NewPassword")]
        public required string ConfirmNewPassword {get ; set;}
        

    }
}
