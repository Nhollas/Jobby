using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities;

public class Contact : BaseEntity
{
    private Contact()
    {
    }

    public Contact(
        string firstName,
        string lastName,
        string jobTitle,
        Social social,
        Guid boardId,
        string ownerId,
        string[] companies,
        string[] emails,
        string[] phones)
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Social = social;
        BoardFk = boardId;
        OwnerId = ownerId;
        Companies = companies;
        Emails = emails;
        Phones = phones;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Social Social { get; set; }
    public string OwnerId { get; set; }
    public string[] Companies { get; set; }
    public string[] Emails { get; set; }
    public string[] Phones { get; set; }

    public Board Board { get; set; }
    public Guid BoardFk { get; set; }

    public ICollection<JobContact> JobContacts { get; set; }
}
