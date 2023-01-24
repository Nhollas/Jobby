﻿using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Board : Entity
{
    private List<JobList> _jobLists = new();
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
        List<JobList> jobLists)
        : base(id, createdDate, ownerId)
    {
        _jobLists = jobLists;
        Name = name;
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<JobList> JobLists => _jobLists;

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
        foreach (var jobList in _jobLists)
        {
            if (jobListIndexes.ContainsKey(jobList.Id))
            {
                jobList.SetIndex(jobListIndexes[jobList.Id]);
            }
        }
    }

    public bool BoardOwnsJob(Guid jobId)
    {
        return JobLists
            .SelectMany(x => x.Jobs
            .Where(x => x.Id == jobId))
            .Any();
    }

    public bool BoardOwnsJoblist(Guid jobListId)
    {
        return JobLists
            .Select(x => x.Id == jobListId)
            .Any();
    }

    public bool BoardOwnsJobs(List<Guid> jobIds)
    {
        return JobLists
            .SelectMany(x => x.Jobs
            .Where(x => jobIds.Contains(x.Id)))
            .Any();
    }
}
