
namespace AttendanceTrackingApi.Domain.Dtos.Attendance
{
    public class GetAttendanceResponseDto
    {
        public int Id {get ; set;}
        public int EmployeeId {get ; set;} 
        public string EmployeeFirstName {get ; set; } = string.Empty;
        public string EmployeeLastName {get ; set; } = string.Empty;
        public string EmployeeDepartment {get ; set; } = string.Empty;
        public DateTime CheckInTime {get ; set;}
        public DateTime? CheckOutTime {get ; set;}
        public DateOnly AttendanceDate {get ; set;}
    }
}