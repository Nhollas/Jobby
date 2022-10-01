using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Jobby.Client.Pages;

public class LoginModel : PageModel
{
    private readonly IAuthFeaturesService _authService;

    public LoginModel(IAuthFeaturesService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    public LoginViewModel Login { get; set; }

    public void OnGet()
    {
        Login = new();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _authService.Authenticate(Login);

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, result.Username),
            new Claim(ClaimTypes.NameIdentifier, result.Id),
        };

        ClaimsIdentity identity = new(claims, "Auth-Cookie");
        ClaimsPrincipal claimsPrincipal = new(identity);

        Response.Cookies.Append("Access-Token", result.Token, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        await HttpContext.SignInAsync("Auth-Cookie", claimsPrincipal);

        return RedirectToPage("/Dashboard");
    }
}
