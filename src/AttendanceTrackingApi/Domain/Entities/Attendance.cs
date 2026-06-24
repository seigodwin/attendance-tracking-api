
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingApi.Domain.Entities
{
    public class Attendance
    {
        [Key]
        public int Id {get ; set;}
        
        [ForeignKey("Employee")]
        public int EmployeeId {get ; set;} 
        public Employee Employee {get ; set;} = null!;
        public DateTime CheckInTime {get ; set;}
        public DateTime? CheckOutTime {get ; set;}
        public DateOnly AttendanceDate {get ; set;}
    }
}