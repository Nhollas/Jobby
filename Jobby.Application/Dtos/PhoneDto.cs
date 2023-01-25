namespace Jobby.Application.Dtos;
public sealed record PhoneDto{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public int Type { get; set; }
    public Guid ContactId { get; set; }
}
