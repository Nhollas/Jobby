using Jobby.Application.Dtos.Common;

namespace Jobby.Application.Dtos;

public class JobListDto : BaseDto
{
    public string Name { get; set; }
    public int Count { get; set; }
    public List<JobDto> Jobs { get; set; }
}
