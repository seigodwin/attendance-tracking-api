using System.ComponentModel.DataAnnotations;

public class RegisterAdminResponseDto
{
    [Key]
    public int id {get;set;}
    [MaxLength(50)]
    public string FirstName {get;set;} = string.Empty;
    [MaxLength(50)]
    public string LastName {get ; set;} = string.Empty;
    [EmailAddress]
    public string Email {get ; set;} = string.Empty;
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber {get;set;} = string.Empty;
    [MaxLength(100)]
    public string Department {get ; set;} = string.Empty;

}