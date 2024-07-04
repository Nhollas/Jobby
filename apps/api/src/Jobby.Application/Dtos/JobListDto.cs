using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public  record JobListDto : EntityDto
{
    public string Name { get; set; }
    public List<JobDto> Jobs { get; set; }
    public string BoardReference { get; set; }
}
