using System.ComponentModel.DataAnnotations;

public class RegisterAdminRequestDto
{
 
    [MaxLength(50)]
    public string FirstName {get;set;} = string.Empty;
    [MaxLength(50)]
    public string LastName {get ; set;} = string.Empty;
    [EmailAddress]
    [Required]
    public required string Email {get ; set;}   
    [DataType(DataType.PhoneNumber)]    
    public string PhoneNumber {get;set;} = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public required string Password {get ;set;}
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfrimPassword {get ;set;}

}