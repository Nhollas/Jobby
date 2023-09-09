using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;

public sealed record UpdateBoardCommand
    (Guid Id, string Name) : IRequest<BaseResult<UpdateBoardResponse, UpdateBoardOutcomes>>;
