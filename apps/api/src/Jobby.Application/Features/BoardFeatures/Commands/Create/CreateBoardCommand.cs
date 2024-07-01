using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public record CreateBoardCommand(string Name) : IRequest<IDispatchResult<BoardDto>>;
