using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Board : Entity
{
    private readonly List<JobList> _lists = new();
    private readonly List<Activity> _activities = new();
    private readonly List<Job> _jobs = new();
    private readonly List<Contact> _contacts = new();

    private Board()
    {

    }

    private Board(
        Guid id,
        string reference,
        DateTime createdDate,
        string ownerId,
        string name)
        : base(id, reference, createdDate, ownerId)
    {
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
        string name)
    {
        Board board = new(
            id,
            reference: EntityReferenceProvider<Board>.CreateReference(),
            createdDate,
            ownerId,
            name);

        return board;
    }

    public void SetBoardName(string name)
    {
        Name = name;
    }

    public void ArrangeJobLists(Dictionary<string, int> jobListIndexes)
    {
        foreach (JobList list in _lists.Where(list => jobListIndexes.ContainsKey(list.Reference)))
        {
            list.SetIndex(jobListIndexes[list.Reference]);
        }
    }

    public bool BoardOwnsJob(string jobReference)
    {
        return Lists
            .SelectMany(list => list.Jobs
            .Where(job => job.Reference == jobReference))
            .Any();
    }

    public bool BoardOwnsList(string jobListReference)
    {
        return Lists
            .Select(list => list.Reference == jobListReference)
            .Any();
    }

    public void SetJobLists(List<JobList> defaultJobLists)
    {
        _lists.AddRange(defaultJobLists);
    }
}
