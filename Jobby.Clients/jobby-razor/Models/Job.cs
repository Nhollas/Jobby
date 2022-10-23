namespace Jobby.Client.Models;

public class Job
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string Colour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<Note> Notes { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Activity> Activities { get; set; }
}
