using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Delete;
public record DeleteListCommand(string ListReference) : IRequest<IDispatchResult<DeleteListResponse>>;