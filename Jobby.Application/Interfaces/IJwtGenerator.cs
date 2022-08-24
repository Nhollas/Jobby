using Jobby.Domain.Entities.Common;

namespace Jobby.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}
