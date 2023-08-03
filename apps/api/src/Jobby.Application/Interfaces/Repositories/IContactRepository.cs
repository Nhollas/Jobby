using Jobby.Domain.Entities;

namespace Jobby.Application.Interfaces.Repositories;

public interface IContactRepository
{
    Task ClearBoardsAsync(Guid boardId, CancellationToken cancellationToken);
    Task ClearJobsAsync(Contact contact, CancellationToken cancellationToken);
}