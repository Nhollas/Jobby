using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobby.Application.Contracts.Job;

public sealed record CreateJobResponse
{
	public CreateJobResponse(
        Guid id,
        DateTime createdDate,
        DateTime lastUpdated,
        string company,
        string title)
	{
        Id = id;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
        Company = company;
        Title = title;
	}

    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public string Company { get; private set; }
    public string Title { get; private set; }
}
