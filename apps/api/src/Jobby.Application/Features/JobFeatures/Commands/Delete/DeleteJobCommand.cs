using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Delete;

public record DeleteJobCommand(string JobReference) : IRequest<IDispatchResult<DeleteJobResponse>>;
