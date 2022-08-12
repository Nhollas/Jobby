using Jobby.Core.Dtos.Common;

namespace Jobby.Core.Dtos;

public class ContactDto : BaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Company { get; set; }
    public SocialDto Social { get; set; }
    public Guid[] Emails { get; set; }
    public Guid[] Phones { get; set; }
}

public class SocialDto
{
    public string TwitterUri { get; set; }
    public string FacebookUri { get; set; }
    public string LinkedInUri { get; set; }
    public string GithubUri { get; set; }
}
