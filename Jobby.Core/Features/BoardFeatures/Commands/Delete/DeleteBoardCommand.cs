using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Delete;

public class DeleteBoardCommand : IRequest
{
    public Guid BoardId { get; set; }

    public DeleteBoardCommand(Guid id)
    {
        BoardId = id;
    }
}
