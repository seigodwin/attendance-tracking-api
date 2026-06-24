using System.ComponentModel.DataAnnotations;

public class UpdateEmployeeRequestDto
{
    [MaxLength(50)]
    public string FirstName {get;set;} = string.Empty;
    [MaxLength(50)]
    public string LastName {get ; set;} = string.Empty;
    [EmailAddress]
    [Required]
    public required string Email {get ; set;} 
    [Required]
    [MaxLength(30)]
    public required string StaffId {get ; set;}
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber {get;set;} = string.Empty;
    [MaxLength(100)]
    public string Department {get ; set;} = string.Empty;

}