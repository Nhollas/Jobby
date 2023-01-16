using Jobby.Application.Interfaces.Services;

namespace Jobby.Application.Services;
internal class GuidProvider : IGuidProvider
{
    public Guid Id => Guid.NewGuid();
}
