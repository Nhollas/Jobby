using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Contact : BaseEntity
{
    private readonly List<Job> _jobs = new();
    private readonly List<Company> _companies = new();
    private readonly List<Email> _emails = new();
    private readonly List<Phone> _phones = new();
    private readonly List<JobContact> _jobContacts = new();

    private Contact()
    {

    }

    private Contact(
        DateTime createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        Social socials,
        Guid boardId,
        List<Company> companies,
        List<Email> emails,
        List<Phone> phones)
        : base(createdDate, ownerId)
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Socials = socials;
        BoardFk = boardId;
        _companies = companies;
        _emails = emails;
        _phones = phones;
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string JobTitle { get; private set; }

    public Social Socials { get; private set; }

    public IReadOnlyCollection<Job> Jobs => _jobs;

    public IReadOnlyCollection<Company> Companies => _companies;

    public IReadOnlyCollection<Email> Emails => _emails;

    public IReadOnlyCollection<Phone> Phones => _phones;


    // Database Relationship Properties
    public ICollection<JobContact> JobContacts => _jobContacts;
    public virtual Board Board { get; set; }
    public Guid BoardFk { get; set; }

    public static Contact Create(
        DateTime createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        Social socials,
        Guid boardId,
        List<Company> companies,
        List<Email> emails,
        List<Phone> phones)
    {
        var contact = new Contact(
            createdDate,
            ownerId,
            firstName,
            lastName,
            jobTitle,
            socials,
            boardId,
            companies,
            emails,
            phones);

        return contact;
    }
}
