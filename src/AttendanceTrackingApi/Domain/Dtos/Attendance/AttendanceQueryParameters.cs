
namespace AttendanceTrackingApi.Domain.Dtos.Attendance
{
    public class AttendanceQueryParameters
    {
        public string DepartmentName {get;set;} = string.Empty;
        public DateOnly? Date {get ; set;}
        public DateOnly? StartDate {get ; set;}
        public DateOnly? EndDate {get ; set;}
    }
}