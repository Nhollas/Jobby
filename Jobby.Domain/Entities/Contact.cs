using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Contact : Entity
{
    private readonly List<Job> _jobs = new();
    private readonly List<Company> _companies = new();
    private readonly List<Email> _emails = new();
    private readonly List<Phone> _phones = new();

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
    public List<JobContact> JobContacts { get; set; }
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
            companies,
            emails,
            phones);

        return contact;
    }

    public void Update(
        string firstName,
        string lastName,
        string jobTitle,
        string location,
        Social socials
        )
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Location = location;
        Socials = socials;

        _companies.Clear();
        _emails.Clear();
        _phones.Clear();
        _jobs.Clear();
    }

    public void UpdateCompanies(List<Company> companies)
    {
        _companies.Clear();

        foreach (var company in companies)
        {
            _companies.Add(company);
        }
    }

    public void UpdateEmails(List<Email> emails)
    {
        _emails.Clear();

        foreach (var email in emails)
        {
            _emails.Add(email);
        }
    }

    public void UpdatePhones(List<Phone> phones)
    {
        _phones.Clear();

        foreach (var phone in phones)
        {
            _phones.Add(phone);
        }
    }

    public void SetJobs(List<Job> jobs)
    {
        _jobs.Clear();

        foreach (var job in jobs)
        {
            _jobs.Add(job);
        }
    }
}
