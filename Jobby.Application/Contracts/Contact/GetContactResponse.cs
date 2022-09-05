using Jobby.Application.Dtos;

namespace Jobby.Application.Contracts.Contact;
public sealed record GetContactResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public PreviewBoardDto Board { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public List<PreviewJobDto> Jobs { get; set; }
    public SocialDto Socials { get; set; }
    public List<PhoneDto> Phones { get; set; }
    public List<EmailDto> Emails { get; set; }
    public List<CompanyDto> Companies { get; set; }
}
