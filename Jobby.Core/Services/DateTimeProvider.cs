using Jobby.Core.Interfaces;

namespace Jobby.Core.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
