using Jobby.Application.Contracts.Auth;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Primitives;
using Microsoft.AspNetCore.Identity;

namespace Jobby.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private ApplicationUser _user;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
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

                return new AuthenticateResponse(_user.FullName, _user.Email, token);
            }

            throw new NotImplementedException();
        }

        throw new NotImplementedException();
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
                FirstName = request.FirstName,
                LastName = request.LastName
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
