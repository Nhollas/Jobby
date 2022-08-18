using Jobby.Core.Contracts.Auth;
using Jobby.Core.Entities.Common;
using Jobby.Core.Exceptions;
using Jobby.Core.Exceptions.Auth;
using Jobby.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Jobby.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private ApplicationUser _user;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticateResponse> Login(AuthenticateRequest request)
    {
        _user = await _userManager.FindByNameAsync(request.Username);

        if (_user != null)
        {
            bool validPassword = await _userManager.CheckPasswordAsync(_user, request.Password);

            if (validPassword)
            {
                string token = _jwtTokenGenerator.GenerateToken(_user);

                return new AuthenticateResponse(token, _user.Id, _user.NormalizedUserName);
            }

            throw new InvalidUserOrPassException();
        }

        throw new InvalidUserOrPassException();
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        ApplicationUser emailTaken = await _userManager.FindByEmailAsync(request.Email);
        ApplicationUser userNameTaken = await _userManager.FindByNameAsync(request.Username);

        if (emailTaken == null && userNameTaken == null)
        {
            ApplicationUser newUser = new()
            {
                Email = request.Email,
                UserName = request.Username,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                return new RegisterResponse(newUser.UserName, newUser.Email);
            }

            throw new BadRequestException("There was an error creating your account, please try again.");
        }
        throw new BadRequestException("There was an error creating your account, please try again.");
    }
}
