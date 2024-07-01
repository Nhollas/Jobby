using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Delete;

public record DeleteActivityCommand(string ActivityReference) : IRequest<IDispatchResult<DeleteActivityResponse>>;
