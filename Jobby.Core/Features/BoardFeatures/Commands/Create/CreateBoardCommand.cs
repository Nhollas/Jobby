using Jobby.Application.Abstractions.Messaging;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public sealed record CreateBoardCommand(string Name) : ICommand<Guid>;
