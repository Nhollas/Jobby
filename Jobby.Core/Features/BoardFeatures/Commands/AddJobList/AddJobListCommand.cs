using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.AddJobList;
public class AddJobListCommand : IRequest
{
    public Guid BoardId { get; }

    public AddJobListCommand(Guid boardId)
    {
        BoardId = boardId;
    }
}
