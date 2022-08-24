using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

public sealed record DeleteBoardCommand(Guid BoardId) : IRequest;