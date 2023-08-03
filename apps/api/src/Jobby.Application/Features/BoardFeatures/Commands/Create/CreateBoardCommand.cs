using Jobby.Application.Contracts.Board;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public sealed record CreateBoardCommand(string Name) : IRequest<CreateBoardResponse>;
