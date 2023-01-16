using Jobby.Application.Contracts.Job;
using Jobby.Application.Contracts.JobList;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Create;
internal sealed record CreateJobListCommand : IRequest<CreateJobListResponse>
{
    public Guid BoardId { get; set; }
    public string Name { get; set; }
}
