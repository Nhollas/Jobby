using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update;

public sealed record UpdateBoardCommand(Guid BoardId, string BoardName) : IRequest;
