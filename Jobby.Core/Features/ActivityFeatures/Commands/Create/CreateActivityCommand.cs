using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Commands.Create;

public class CreateActivityCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
}
