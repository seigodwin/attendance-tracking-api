
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class Admin : IdentityUser
{
    [MaxLength(50)]
    public string FirstName {get;set;} = string.Empty;
    [MaxLength(50)]
    public string LastName {get ; set;} = string.Empty;
}