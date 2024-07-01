using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

public record CreateJobCommand(
    string Company, 
    string Title, 
    string BoardReference, 
    string JobListReference) 
    : IRequest<IDispatchResult<JobDto>>;