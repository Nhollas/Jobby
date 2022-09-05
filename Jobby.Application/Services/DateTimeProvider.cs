using Jobby.Application.Interfaces.Services;

namespace Jobby.Application.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
