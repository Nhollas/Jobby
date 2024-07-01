using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Jobby.Application.Services;


public interface IUserService
{
    public string UserId();
}

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId()
    {
        return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
    }
}
