using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public sealed record CreateBoardCommand(string Name) : IRequest<IDispatchResult<BoardDto>>;
