using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

public record DeleteBoardCommand(string BoardReference) : IRequest<IDispatchResult<DeleteBoardResponse>>;