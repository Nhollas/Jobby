using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update;

public sealed record UpdateBoardCommand(Guid Id, string Name) : IRequest;
