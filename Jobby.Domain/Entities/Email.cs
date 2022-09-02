namespace Jobby.Domain.Entities;
public class Email
{
    public Email(
    Guid id,
    string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
}
