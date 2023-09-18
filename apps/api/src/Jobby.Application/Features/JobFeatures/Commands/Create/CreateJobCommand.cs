using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

public sealed record CreateJobCommand : IRequest<BaseResult<JobDto, CreateJobOutcomes>>
{
    public string Company { get; set; }
    public string Title { get; set; }
    public Guid BoardId { get; set; }
    public Guid JobListId { get; set; }
}
