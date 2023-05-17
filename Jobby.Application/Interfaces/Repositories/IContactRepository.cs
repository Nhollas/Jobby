namespace Jobby.Application.Interfaces.Repositories;

public interface IContactRepository
{
    Task ClearBoardsAsync(Guid boardId, CancellationToken cancellationToken);
}