using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Job : Entity
{
    public Job(){}

    private Job(
        string reference,
        DateTimeOffset createdDate,
        string ownerId,
        string company,
        string jobTitle,
        int index,
        JobList jobList,
        Board board
        )
        : base(reference, createdDate, ownerId)

    {
        Company = company;
        Title = jobTitle;
        Index = index;
        JobList = jobList;
        Board = board;
        BoardReference = board.Reference;
        JobListReference = jobList.Reference;
    }

    public string Company { get; private set; }
    public string Title { get; private set; }
    public string PostUrl { get; private set; } = string.Empty;
    public double Salary { get; private set; } = 0;
    public string Location { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTimeOffset? Deadline { get; private set; } 
    public int Index { get; private set; }
    public List<Note> Notes { get; } = new();
    public List<Activity> Activities { get; } = new();
    public List<Contact> Contacts { get; } = new();


    // Database Relationship Properties
    public JobList JobList { get; set; }
    public List<JobContact> JobContacts { get; } = new();
    public Guid JobListId { get; set; }
    public string JobListReference { get; private set; }
    public Board Board { get; private set; }
    public string BoardReference { get; private set; }
    public Guid BoardId { get; private set; }


    public static Job Create(
        DateTimeOffset createdDate,
        string ownerId,
        string company,
        string jobTitle,
        int index,
        JobList jobList,
        Board board)
    {
        Job job = new(
            reference: EntityReferenceProvider<Job>.CreateReference(),
            createdDate,
            ownerId,
            company,
            jobTitle,
            index,
            jobList,
            board);

        return job;
    }

    public void SetJobList(JobList jobList)
    {
        JobList = jobList;
        JobListId = jobList.Id;
        JobListReference = jobList.Reference;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }
}
