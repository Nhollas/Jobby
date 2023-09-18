using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public sealed record BoardDto : EntityDto
{
    public string Name { get; set; }

    public List<JobListDto> Lists { get; set; }

    public List<ActivityDto> Activities { get; set; }

    public List<ContactDto> Contacts { get; set; }
}