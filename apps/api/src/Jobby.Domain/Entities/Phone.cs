using Jobby.Domain.Primitives;
using Jobby.Domain.Static;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Domain.Entities;
public class Phone: Entity
{
    private Phone(){}

    private Phone(
        Guid id,
        string reference,
        DateTime createdDate,
        string ownerId,
        string number,
        PhoneType type,
        Contact contact)
        : base(id, reference, createdDate, ownerId)
    {
        Contact = contact;
        ContactId = contact.Id;
        ContactReference = contact.Reference;
        Number = number;
        Type = type;
    }
    
    public static Phone Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string number,
        PhoneType type,
        Contact contact)
    {
        Phone phone = new Phone(
            id,
            reference: EntityReferenceProvider<Phone>.CreateReference(),
            createdDate,
            ownerId,
            number,
            type,
            contact);

        return phone;
    }
    
    public string Number { get; set; }
    public PhoneType Type { get; set; }

    
    // Database Relationship Properties
    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
    public string ContactReference { get; set; }
}
