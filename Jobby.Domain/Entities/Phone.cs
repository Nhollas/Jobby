namespace Jobby.Domain.Entities;
public class Phone
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public PhoneType Type { get; set; }
}
