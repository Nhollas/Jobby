namespace Jobby.Core.Dtos;

public class BoardDto
{
    public string Name { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
    public List<JobListDto> JobList { get; set; }
}
