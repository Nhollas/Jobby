using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Delete;

public sealed record DeleteJobCommand(Guid JobId) : IRequest<BaseResult<DeleteJobResponse, DeleteJobOutcomes>>;
