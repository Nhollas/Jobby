using Jobby.Application.Contracts.Job;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

public sealed record CreateJobCommand : IRequest<CreateJobResponse>
{
    public string Company { get; set; }
    public string Title { get; set; }
    public string Colour { get; set; } = string.Empty;
    public Guid BoardId { get; set; }
    public Guid JobListId { get; set; }
}
