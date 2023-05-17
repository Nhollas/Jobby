using Jobby.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Data.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly JobbyDbContext _context;

    public ContactRepository(JobbyDbContext context)
    {
        _context = context;
    }

    public async Task ClearBoardsAsync(Guid boardId, CancellationToken cancellationToken)
    {
        await _context.Contacts
            .Where(contact => contact.BoardId == boardId)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.BoardId,  x => null), cancellationToken);
    }
}