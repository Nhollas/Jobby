using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public sealed record CreateActivityCommand : IRequest<Guid>
{
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
    public string Title { get; set; }
    public int Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
