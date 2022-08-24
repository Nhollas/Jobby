namespace Jobby.Application.Dtos;

public sealed record ContactDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public SocialDto Social { get; set; }
    public List<EmailDto> Emails { get; set; }
    public List<PhoneDto> Phones { get; set; }
    public List<CompanyDto> Companies { get; set; }
}