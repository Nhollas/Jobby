using Jobby.Application.Contracts.Auth;
using Jobby.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register", Name = "Register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest request)
    {
        var result = await _authenticationService.Register(request);

        return Ok(result);
    }

    [HttpPost("login", Name = "Login")]
    public async Task<ActionResult<AuthenticateResponse>> Login([FromBody] AuthenticateRequest request)
    {
        var result = await _authenticationService.Login(request);

        return Ok(result);
    }
}
