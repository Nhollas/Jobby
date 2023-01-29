namespace Jobby.Application.Contracts.JobList;

public sealed record JobListDictionaryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}