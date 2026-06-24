
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingApi.Domain.Entities
{
    public class Attendance
    {
        [Key]
        public int id {get ; set;}
        public Employee? employee {get ; set;}
        [ForeignKey("Employee")]
        public int EmployeeId {get ; set;} 
        public DateTime? CheckInTime {get ; set;}
        public DateTime? CheckOutTime {get ; set;}
    }
}