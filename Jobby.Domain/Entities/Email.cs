using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Domain.Entities;
public class Email
{
    public Email(
    Guid id,
    string name,
    EmailType type)
    {
        Id = id;
        Name = name;
        Type = type;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public EmailType Type { get; set; }

    
    // Database Relationship Properties
    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
}
