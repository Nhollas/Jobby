using Jobby.Domain.Primitives;
using Jobby.Domain.Static;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Domain.Entities;
public class Email: Entity
{
    private Email(){}
    
    private Email(
        Guid id,
        string reference,
        DateTime createdDate,
        string ownerId,
        string name,
        EmailType type,
        Contact contact)
        : base(id, reference, createdDate, ownerId)
    {
        Contact = contact;
        ContactId = contact.Id;
        ContactReference = contact.Reference;
        Name = name;
        Type = type;
    }
    
    public static Email Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        EmailType type,
        Contact contact)
    {
        Email phone = new Email(
            id,
            reference: EntityReferenceProvider<Email>.CreateReference(),
            createdDate,
            ownerId,
            name,
            type,
            contact);

        return phone;
    }
    
    public string Name { get; set; }
    public EmailType Type { get; set; }

    
    // Database Relationship Properties
    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
    public string ContactReference { get; set; }
}
