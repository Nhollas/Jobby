using Jobby.Core.Contracts.Auth;
using Jobby.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authenticationService.Register(request);

        return Ok(result);
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateRequest request)
    {
        var result = await _authenticationService.Login(request);

        return Ok(result);
    }
}
