namespace Jobby.Application.Dtos;
public sealed record PreviewContactDto
{
    public Guid Id { get; set; }
    public PreviewBoardDto Board { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public SocialDto Socials { get; set; }
    public List<PhoneDto> Phones { get; set; }
    public List<EmailDto> Emails { get; set; }
    public List<CompanyDto> Companies { get; set; }
}
