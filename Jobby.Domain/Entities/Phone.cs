namespace Jobby.Domain.Entities;
public class Phone
{
    public Phone(
    Guid id,
    string number,
    PhoneType type)
    {
        Id = id;
        Number = number;
        Type = type;
    }

    public Guid Id { get; set; }
    public string Number { get; set; }
    public PhoneType Type { get; set; }

    public Contact Contact { get; set; }
    public Guid ContactId { get; set; }
}
