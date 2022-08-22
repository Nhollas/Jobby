namespace Jobby.Application.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
