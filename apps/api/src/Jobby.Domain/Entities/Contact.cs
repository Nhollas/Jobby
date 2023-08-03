using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Contact : Entity
{
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
        Companies = companies;
        Emails = emails;
        Phones = phones;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string JobTitle { get; private set; }
    public string Location { get; private set; }
    public Social Socials { get; private set; }
    public List<Job> Jobs { get; } = new();
    public List<Company> Companies { get; } = new();
    public List<Email> Emails { get; } = new();
    public List<Phone> Phones { get; } = new();


    // Database Relationship Properties
    public List<JobContact> JobContacts { get; } = new();
    public Board Board { get; private set; }
    public Guid? BoardId { get; private set; }


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
    
    public void SetBoard(Board board)
    {
        Board = board;
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

        Companies.Clear();
        Emails.Clear();
        Phones.Clear();
        Jobs.Clear();
    }

    public void UpdateCompanies(List<Company> companies)
    {
        Companies.Clear();

        foreach (var company in companies)
        {
            Companies.Add(company);
        }
    }

    public void UpdateEmails(List<Email> emails)
    {
        Emails.Clear();

        foreach (var email in emails)
        {
            Emails.Add(email);
        }
    }

    public void UpdatePhones(List<Phone> phones)
    {
        Phones.Clear();

        foreach (var phone in phones)
        {
            Phones.Add(phone);
        }
    }

    public void SetJobs(List<Job> jobs)
    {
        Jobs.Clear();
        JobContacts.Clear();

        foreach (var job in jobs)
        {
            JobContacts.Add(new JobContact(this, job));
            Jobs.Add(job);
        }
    }
}
