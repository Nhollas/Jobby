namespace Jobby.Core.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
