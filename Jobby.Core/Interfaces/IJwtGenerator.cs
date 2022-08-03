using Jobby.Core.Entities.Common;

namespace Jobby.Core.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}
