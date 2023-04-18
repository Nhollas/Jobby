namespace Jobby.Application.Interfaces.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string username, long expiresAt);
}
