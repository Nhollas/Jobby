namespace Jobby.Application.Contracts.JobList;
public sealed record CreateJobListResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public int Index { get; set; }

}

