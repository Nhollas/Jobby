using Jobby.Application.Interfaces;

namespace Jobby.Application.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
