using Jobby.Application.Contracts.JobList;

namespace Jobby.Application.Contracts.Board;

public sealed record BoardDictionaryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<JobListDictionaryResponse> JobLists { get; set; }
}