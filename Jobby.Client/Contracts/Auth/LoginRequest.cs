using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.Contracts.Auth;

public class LoginRequest
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}
