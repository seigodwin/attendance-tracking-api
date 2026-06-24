
using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingApi.Domain.Dtos.Attendance
{
    public class CheckInOrOutRequestDto
    {
        [EmailAddress]
        public required string Email {get ; set; }
        [MaxLength(30)]
        public required string StaffId {get; set;}
    }
}