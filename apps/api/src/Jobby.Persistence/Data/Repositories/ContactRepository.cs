using Jobby.Application.Interfaces.Repositories;
using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Data.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly JobbyDbContext _context;

    public ContactRepository(JobbyDbContext context)
    {
        _context = context;
    }

    public async Task ClearBoardsAsync(string boardReference, CancellationToken cancellationToken)
    {
        await _context.Contacts
            .Where(contact => contact.BoardReference == boardReference)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.BoardId,  x => null).SetProperty(x => x.BoardReference, x => null), cancellationToken);
    }

    public async Task ClearJobsAsync(Contact contact, CancellationToken cancellationToken)
    {
        await _context.JobContacts.Where(jobContact => jobContact.ContactId == contact.Id).ExecuteDeleteAsync(cancellationToken);
    }
}