namespace Jobby.Core.Dtos;

public class JobListDto
{
    public string Name { get; set; }
    public int Count { get; set; }
    public List<JobDto> Jobs { get; set; }
}
