using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;

public record UpdateBoardCommand
    (string BoardReference, string Name) : IRequest<IDispatchResult<BoardDto>>;
