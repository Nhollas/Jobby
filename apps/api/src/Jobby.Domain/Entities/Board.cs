using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Board : Entity
{
    private List<JobList> _lists = new();
    private readonly List<Activity> _activities = new();
    private readonly List<Job> _jobs = new();
    private readonly List<Contact> _contacts = new();

    public Board()
    {

    }

    private Board(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        List<JobList> lists)
        : base(id, createdDate, ownerId)
    {
        _lists = lists;
        Name = name;
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<JobList> Lists => _lists;

    public IReadOnlyCollection<Activity> Activities => _activities;

    public IReadOnlyCollection<Job> Jobs => _jobs;

    public IReadOnlyCollection<Contact> Contacts => _contacts;

    public static Board Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        List<JobList> jobLists)
    {
        var board = new Board(
            id,
            createdDate,
            ownerId,
            name,
            jobLists);

        return board;
    }

    public void SetBoardName(string name)
    {
        Name = name;
    }

    public void ArrangeJobLists(Dictionary<Guid, int> jobListIndexes)
    {
        foreach (var list in _lists)
        {
            if (jobListIndexes.ContainsKey(list.Id))
            {
                list.SetIndex(jobListIndexes[list.Id]);
            }
        }
    }

    public bool BoardOwnsJob(Guid jobId)
    {
        return Lists
            .SelectMany(x => x.Jobs
            .Where(x => x.Id == jobId))
            .Any();
    }

    public bool BoardOwnsJoblist(Guid jobListId)
    {
        return Lists
            .Select(x => x.Id == jobListId)
            .Any();
    }

    public bool BoardOwnsJobs(List<Guid> jobIds)
    {
        return Lists
            .SelectMany(x => x.Jobs
            .Where(x => jobIds.Contains(x.Id)))
            .Any();
    }
}
