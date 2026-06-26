
using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingApi.Domain.Dtos.Attendance
{
    public class CheckInOrOutRequestDto
    {
        [Required]
        [EmailAddress]
        public required string Email {get ; set; }
        [MaxLength(30)]
        [Required]
        public required string StaffId {get; set;}
    }
}