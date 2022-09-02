using Jobby.Domain.Primitives;

namespace Jobby.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}
