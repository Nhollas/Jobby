using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.Contracts.Auth;

public class RegisterRequest
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }
}
