using Jobby.Core.Entities.Common;
using Jobby.Core.Interfaces;

namespace Jobby.Core.Entities.ContactAggregate;

public class Contact : BaseEntity, IAggregateRoot
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Social Social { get; set; }
    public Guid BoardId { get; set; }
    public string OwnerId { get; set; }

    public string JobIds { get; set; }
    public string Companies { get; set; }
    public string Emails { get; set; }
    public string Phones { get; set; }

    public IReadOnlyCollection<string> _JobIds;
    public IReadOnlyCollection<string> _Emails;
    public IReadOnlyCollection<string> _Companies;
    public IReadOnlyCollection<string> _Phones;

    private Contact()
    {

    }

    public Contact(
        string firstName, 
        string lastName,
        string jobTitle,
        Social social,
        Guid boardId,
        string ownerId)
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Social = social;
        BoardId = boardId;
        OwnerId = ownerId;
    }

    public void Split()
    {
        _JobIds = JobIds.Split(';');
        _Emails = Emails.Split(';');
        _Companies = Companies.Split(';');
        _Phones = Phones.Split(';');
    }

    public void Join(string[] jobIds, string[] companies, string[] emails, string[] phones)
    {
        JobIds = string.Join(";", jobIds);
        Companies = string.Join(";", companies);
        Emails = string.Join(";", emails);
        Phones = string.Join(";", phones);
    }
}
