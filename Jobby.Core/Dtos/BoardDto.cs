using Jobby.Application.Dtos.Common;

namespace Jobby.Application.Dtos;

public class BoardDto : BaseDto
{
    public string Name { get; set; }
    public List<JobListDto> JobList { get; set; }
    public List<ActivityDto> Activities { get; set; }
    public List<ContactDto> Contacts { get; set; }
}
