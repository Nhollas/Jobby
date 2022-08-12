using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.DeleteJobList;
public class DeleteJobListCommand : IRequest
{
    public Guid BoardId { get; set; }
    public Guid ListId { get; set; }

    public DeleteJobListCommand(Guid boardId, Guid listId)
    {
        BoardId = boardId;
        ListId = listId;
    }
}
