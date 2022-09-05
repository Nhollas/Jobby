using Jobby.Domain.Primitives;

namespace Jobby.Application.Interfaces.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}
