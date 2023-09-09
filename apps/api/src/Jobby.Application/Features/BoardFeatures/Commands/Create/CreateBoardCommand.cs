using Jobby.Application.Contracts.Board;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public sealed record CreateBoardCommand(string Name) : IRequest<BaseResult<CreateBoardResponse, CreateBoardOutcomes>>;
