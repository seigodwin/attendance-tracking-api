using System.ComponentModel.DataAnnotations;
using AttendanceTrackingApi.Domain.Entities;

public class Employee
{
    [Key]
    public int Id {get;set;}
    [MaxLength(50)]
    public string FirstName {get;set;} = string.Empty;
    [MaxLength(50)]
    public string LastName {get ; set;} = string.Empty;
    [EmailAddress]
    public string Email {get ; set;} = string.Empty;
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber {get;set;} = string.Empty;
    [MaxLength(100)]
    public string Department {get;set;} = string.Empty;
    public List<Attendance> Attendances {get ; set;} = new ();

}