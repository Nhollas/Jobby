using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

public sealed record DeleteBoardCommand(string BoardReference) : IRequest<BaseResult<DeleteBoardResponse, DeleteBoardOutcomes>>;