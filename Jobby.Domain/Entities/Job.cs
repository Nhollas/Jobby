﻿using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Job : Entity
{
    private readonly List<Activity> _activities = new();
    private readonly List<Contact> _contacts = new();
    private readonly List<Note> _notes = new();
    private readonly List<JobContact> _jobContacts = new();

    public Job()
    {

    }

    private Job(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string company,
        string jobTitle,
        JobList jobList)
        : base(id, createdDate, ownerId)

    {
        Company = company;
        Title = jobTitle;
        JobList = jobList;
    }

    public string Company { get; private set; }

    public string Title { get; private set; }

    public string PostUrl { get; private set; }

    public double Salary { get; private set; }

    public string Location { get; private set; }

    public string Colour { get; private set; }

    public string Description { get; private set; }

    public DateTime Deadline { get; private set; }

    public IReadOnlyCollection<Note> Notes => _notes;

    public IReadOnlyCollection<Activity> Activities => _activities;

    public IReadOnlyCollection<Contact> Contacts => _contacts;


    // Database Relationship Properties
    public ICollection<JobContact> JobContacts => _jobContacts;
    public virtual JobList JobList { get; set; }
    public Guid JobListId { get; set; }

    public static Job Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string company,
        string jobTitle,
        JobList jobList)
    {
        var job = new Job(
            id,
            createdDate,
            ownerId,
            company,
            jobTitle,
            jobList);

        return job;
    }

    public void AddActivity(Activity activity)
    {
        _activities.Add(activity);
    }

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void AddNote(Note note)
    {
        _notes.Add(note);
    }
}
