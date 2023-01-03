namespace Jobby.Application.Contracts.Job;

public sealed record CreateJobResponse
{
	public CreateJobResponse(
        Guid id,
        string createdDate,
        DateTime lastUpdated,
        string company,
        string title,
        int index)
	{
        Id = id;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
        Company = company;
        Title = title;
        Index = index;
	}

    public Guid Id { get; private set; }
    public string CreatedDate { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public string Company { get; private set; }
    public string Title { get; private set; }
    public int Index { get; private set; }
}
