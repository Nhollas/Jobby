using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jobby.Client.Controllers;

[AllowAnonymous]
[Route("[controller]/[action]")]
public class AuthController : Controller
{
    private readonly IAuthFeaturesService _authService;

    public AuthController(IAuthFeaturesService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authService.Authenticate(model);

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

        return RedirectToRoute(new
        {
            controller = "Dashboard",
            action = "Index"
        });
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpGet("{username}")]
    public IActionResult Login(string username)
    {
        ViewData["ImportedUsername"] = username;

        return View();
    }

    public IActionResult Login()
    {
        ViewData["ImportedUsername"] = "";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _authService.Register(model);

        return RedirectToAction("Login", new { username = result.Username });
    }

    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("Access-Token");
        await HttpContext.SignOutAsync();

        return RedirectToRoute(new
        {
            controller = "Home",
            action = "Index"
        });
    }
}
