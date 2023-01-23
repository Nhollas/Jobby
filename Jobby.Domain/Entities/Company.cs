namespace Jobby.Domain.Entities;
public class Company
{
    public Company(
        Guid id,
        string name,
        Contact contact)
    {
        Id = id;
        Name = name;
        Contact = contact;
        ContactId = contact.Id;
    }

    public Company(
        Guid id,
        string name)
    {
        Id = id;
        Name = name; 
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    // Database Relationship Properties
    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
}
