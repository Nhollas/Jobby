using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;
public class Company: Entity
{
    private Company(){}
    
    private Company(
        string reference,
        DateTimeOffset createdDate,
        string ownerId,
        string name,
        Contact contact)
        : base(reference, createdDate, ownerId)
    {
        Contact = contact;
        ContactId = contact.Id;
        ContactReference = contact.Reference;
        Name = name;
    }
    
    public static Company Create(
        DateTimeOffset createdDate,
        string ownerId,
        string name,
        Contact contact)
    {
        Company company = new Company(
            reference: EntityReferenceProvider<Company>.CreateReference(),
            createdDate,
            ownerId,
            name,
            contact);

        return company;
    }
    
    public string Name { get; set; }

    // Database Relationship Properties
    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
    public string ContactReference { get; set; }
}
