using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;

public sealed record UpdateBoardCommand
    (string BoardReference, string Name) : IRequest<BaseResult<BoardDto, UpdateBoardOutcomes>>;
