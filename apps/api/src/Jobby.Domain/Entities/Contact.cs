using Jobby.Domain.Dtos.Contact;
using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Contact : Entity
{
    #pragma warning disable CS8618 // Required by Entity Framework
    private Contact(){}

    private Contact(
        string reference,
        DateTimeOffset createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        string location,
        Social socials,
        Board board,
        IEnumerable<string> companies,
        IEnumerable<CreateEmailDto> emails,
        IEnumerable<CreatePhoneDto> phones)
        : base(reference, createdDate, ownerId)
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
    public List<Job> Jobs { get; } = [];
    public List<Company> Companies { get; } = [];
    public List<Email> Emails { get; } = [];
    public List<Phone> Phones { get; } = [];


    public List<JobContact> JobContacts { get; } = [];
    public Board? Board { get; private set; }
    public string? BoardReference { get; private set; }
    public Guid? BoardId { get; private set; }


    public static Contact Create(
        DateTimeOffset createdDate,
        string ownerId,
        string firstName,
        string lastName,
        string jobTitle,
        string location,
        Social socials,
        Board board,
        IEnumerable<string> companies,
        IEnumerable<CreateEmailDto> emails,
        IEnumerable<CreatePhoneDto> phones) 
    {
        Contact contact = new(
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

    public void ReplaceCompanies(List<Company> companies)
    {
        Companies.Clear();

        foreach (Company company in companies)
        {
            Companies.Add(company);
        }
    }
    
    private List<Company> CreateCompanies(IEnumerable<string> companies, DateTimeOffset createdDate)
    {
        return companies.Select(company => Company.Create(createdDate, OwnerId, company, this)).ToList();
    }
    
    private List<Email> CreateEmails(IEnumerable<CreateEmailDto> emails, DateTimeOffset createdDate)
    {
        return emails.Select(email => Email.Create(createdDate, OwnerId, email.Name, (ContactConstants.EmailType)email.Type, this)).ToList();
    }
    
    private List<Phone> CreatePhones(IEnumerable<CreatePhoneDto> phones, DateTimeOffset createdDate)
    {
        return phones.Select(phone => Phone.Create(createdDate, OwnerId, phone.Number, (ContactConstants.PhoneType)phone.Type, this)).ToList();
    }

    public void ReplaceEmails(List<Email> emails)
    {
        Emails.Clear();

        foreach (Email email in emails)
        {
            Emails.Add(email);
        }
    }

    public void ReplacePhones(List<Phone> phones)
    {
        Phones.Clear();

        foreach (Phone phone in phones)
        {
            Phones.Add(phone);
        }
    }

    public void SetJobs(List<Job> jobs)
    {
        Jobs.Clear();
        JobContacts.Clear();

        foreach (Job job in jobs)
        {
            JobContacts.Add(new JobContact(this, job));
            Jobs.Add(job);
        }
    }
}
