using Jobby.Application.Abstractions.Messaging;
using Jobby.Application.Contracts.Board;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public sealed record CreateBoardCommand(string Name) : ICommand<CreateBoardResponse>;
