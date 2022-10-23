using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobby.Client.Pages;

public class RegisterModel : PageModel
{
    private readonly IAuthFeaturesService _authService;

    public RegisterModel(IAuthFeaturesService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    public RegisterViewModel Register { get; set; }

    public void OnGet()
    {
        Register = new();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _authService.Register(Register);

        return RedirectToPage("/Login");
    }
}
