using Jobby.Application.Interfaces.Repositories;
using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Data.Repositories;

public class ContactRepository(JobbyDbContext context) : IContactRepository
{
    public async Task ClearBoardsAsync(Guid boardId, CancellationToken cancellationToken)
    {
        await context.Contacts
            .Where(contact => contact.BoardId == boardId)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.BoardId,  x => null).SetProperty(x => x.BoardReference, x => null), cancellationToken);
    }

    public async Task ClearJobsAsync(Contact contact, CancellationToken cancellationToken)
    {
        await context.JobContacts
            .Where(jobContact => jobContact.ContactId == contact.Id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}