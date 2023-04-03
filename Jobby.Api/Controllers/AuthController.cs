using Jobby.Application.Contracts.Auth;
using Jobby.Application.Interfaces.Services;
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

    [HttpPost("exchange-token/google", Name = "ExchangeGoogleToken")]
    public async Task<ActionResult<ExchangeTokenResponse>> ExchangeGoogleToken([FromBody] ExchangeTokenRequest request)
    {
        var result = await _authenticationService.ExchangeGoogleToken(request);

        return Ok(result);
    }
    
    [HttpPost("exchange-token/github", Name = "ExchangeGithubToken")]
    public async Task<ActionResult<ExchangeTokenResponse>> ExchangeGithubToken([FromBody] ExchangeTokenRequest request)
    {
        var result = await _authenticationService.ExchangeGithubToken(request);

        return Ok(result);
    }
}
