using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Commands.Update;

public class UpdateActivityCommand : IRequest
{
    public Guid ActivityId { get; set; }
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
