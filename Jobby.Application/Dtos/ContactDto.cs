using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public sealed record ContactDto : EntityDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public SocialDto Social { get; set; }
    public List<EmailDto> Emails { get; set; }
    public List<PhoneDto> Phones { get; set; }
    public List<CompanyDto> Companies { get; set; }
    public List<JobDto> Jobs { get; set; }
    public Guid BoardId { get; set; }
}