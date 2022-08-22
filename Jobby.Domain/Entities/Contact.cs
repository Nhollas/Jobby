using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Contact : BaseEntity
{
    private Contact()
    {
    }

    public Contact(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        Social social,
        Guid boardId,
        string[] companies,
        string[] emails,
        string[] phones)
        : base(id, createdDate, ownerId)
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Social = social;
        BoardFk = boardId;
        Companies = companies;
        Emails = emails;
        Phones = phones;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Social Social { get; set; }
    public string[] Companies { get; set; }
    public string[] Emails { get; set; }
    public string[] Phones { get; set; }

    public Board Board { get; set; }
    public Guid BoardFk { get; set; }

    public ICollection<JobContact> JobContacts { get; set; }
}
