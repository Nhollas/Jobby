using Jobby.Client.Models;
using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.ViewModels;

public class ViewJobVM
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Company { get; set; }
    [Display(Name = "Job Title")]
    public string Title { get; set; }
    [Display(Name = "Post URL")]
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string Location { get; set; }
    public string Colour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<Note> Notes { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Activity> Activities { get; set; }
    public BoardPreview Board { get; set; }

    // Additional View Model Properties.
    public Activity AppliedActivity => Activities.Find(x => x.Type == 1);
    public List<Activity> InterviewActivities => Activities.FindAll(x => x.Type == 2 || x.Type == 3 || x.Type == 4);
    public List<Activity> OfferActivities => Activities.FindAll(x => x.Type == 5);
    public Activity AcceptedOfferActivity => Activities.Find(x => x.Type == 6);
}