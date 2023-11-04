using Jobby.Domain.Dtos.Contact;
using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Contact : Entity
{
    private Contact(){}

    private Contact(
        Guid id,
        string reference,
        DateTime createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        string location,
        Social socials,
        Board board,
        List<string> companies,
        List<CreateEmailDto> emails,
        List<CreatePhoneDto> phones)
        : base(id, reference, createdDate, ownerId)
    {
        FirstName = firstName;
        LastName = lastName;
        JobTitle = jobTitle;
        Location = location;
        Socials = socials;
        Board = board;
        BoardReference = board.Reference;
        Companies = CreateCompanies(companies, createdDate);
        Emails = CreateEmails(emails, createdDate);
        Phones = CreatePhones(phones, createdDate);
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
    
    public string BoardReference { get; private set; }
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
        List<string> companies,
        List<CreateEmailDto> emails,
        List<CreatePhoneDto> phones)
    {
        var contact = new Contact(
            id,
            reference: EntityReferenceProvider<Contact>.CreateReference(),
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
        BoardId = board.Id;
        BoardReference = board.Reference;
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
    
    private List<Company> CreateCompanies(IEnumerable<string> companies, DateTime createdDate)
    {
        return companies.Select(company => Company.Create(Guid.NewGuid(), createdDate, OwnerId, company, this)).ToList();
    }
    
    private List<Email> CreateEmails(IEnumerable<CreateEmailDto> emails, DateTime createdDate)
    {
        return emails.Select(email => Email.Create(Guid.NewGuid(), createdDate, OwnerId, email.Name, (ContactConstants.EmailType)email.Type, this)).ToList();
    }
    
    private List<Phone> CreatePhones(IEnumerable<CreatePhoneDto> phones, DateTime createdDate)
    {
        return phones.Select(phone => Phone.Create(Guid.NewGuid(), createdDate, OwnerId, phone.Number, (ContactConstants.PhoneType)phone.Type, this)).ToList();
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
