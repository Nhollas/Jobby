using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
public record MoveJobCommand(
    string JobReference, 
    string JobListReference) 
    : IRequest<IDispatchResult<MoveJobResponse>>;