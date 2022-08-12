using Jobby.Core.Dtos.Common;

namespace Jobby.Core.Dtos;

public class JobDto : BaseDto
{
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string HexColour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Notes { get; set; }
    public List<ContactDto> Contacts { get; set; }
    public List<ActivityDto> Activities { get; set; }
}
