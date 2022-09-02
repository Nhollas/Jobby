using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Contact : Entity
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
        Guid id,
        DateTime createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        string location,
        Social socials,
        Board board,
        List<Job> jobs,
        List<Company> companies,
        List<Email> emails,
        List<Phone> phones)
        : base(id, createdDate, ownerId)
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Location = location;
        Socials = socials;
        Board = board;
        _jobs = jobs;
        _companies = companies;
        _emails = emails;
        _phones = phones;
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string JobTitle { get; private set; }

    public string Location { get; private set; }

    public Social Socials { get; private set; }

    public IReadOnlyCollection<Job> Jobs => _jobs;

    public IReadOnlyCollection<Company> Companies => _companies;

    public IReadOnlyCollection<Email> Emails => _emails;

    public IReadOnlyCollection<Phone> Phones => _phones;


    // Database Relationship Properties
    public IReadOnlyCollection<JobContact> JobContacts => _jobContacts;
    public Board Board { get; private set; }
    public Guid BoardId { get; private set; }

    public static Contact Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        string location,
        Social socials,
        Board board,
        List<Job> jobs,
        List<Company> companies,
        List<Email> emails,
        List<Phone> phones)
    {
        var contact = new Contact(
            id,
            createdDate,
            ownerId,
            firstName,
            lastName,
            jobTitle,
            location,
            socials,
            board,
            jobs,
            companies,
            emails,
            phones);

        return contact;
    }
}
