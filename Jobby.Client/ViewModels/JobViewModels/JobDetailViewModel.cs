using System.ComponentModel.DataAnnotations;
using Jobby.Client.Models.ActivityModels;
using Jobby.Client.Models.ContactModels;
using Jobby.Client.Models.JobModels;
using Jobby.Client.ViewModels.Common;

namespace Jobby.Client.ViewModels.JobViewModels;

public class JobDetailViewModel : BaseViewModel
{
    [Display(Name = "Job Title")]
    public string Title { get; set; }
    public string Company { get; set; }
    [Display(Name = "Post URL")]
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<Note> Notes { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Activity> Activities { get; set; }

    // Additional View Model Properties.
    public Guid BoardId { get; set; }
    public List<ActivityType> ActivityTypes { get; set; }
    public Activity AppliedActivity => Activities.Find(x => x.ActivityType == 1);
    public List<Activity> InterviewActivities => Activities.FindAll(x => x.ActivityType == 2 || x.ActivityType == 3 || x.ActivityType == 4);
    public List<Activity> OfferActivities => Activities.FindAll(x => x.ActivityType == 5);
    public Activity AcceptedOfferActivity => Activities.Find(x => x.ActivityType == 6);

    public void OnGet()
    {
        ActivityTypes = new()
        {
            new ActivityType() {Text="Apply", Value=1, Colour="#0e4e62"},
            new ActivityType() {Text="Phone Screen", Value=2, Colour="#0a9ba2"},
            new ActivityType() {Text="Phone Interview", Value=3, Colour="#0ffff7"},
            new ActivityType() {Text="On Site Interview", Value=4, Colour="#06ddd1"},
            new ActivityType() {Text="Offer Received", Value=5, Colour="#33fda0"},
            new ActivityType() {Text="Accept Offer", Value=6, Colour="#9ce735"},
            new ActivityType() {Text="Prep Cover Letter", Value=7, Colour="#baff8f"},
            new ActivityType() {Text="Prep Resume", Value=8, Colour="#f6fb81"},
            new ActivityType() {Text="Reach Out", Value=9, Colour="#ffd231"},
            new ActivityType() {Text="Get Reference", Value=10, Colour="#ffa437"},
            new ActivityType() {Text="Follow Up", Value=11, Colour="#f27c30"},
            new ActivityType() {Text="Prep For Interview", Value=12, Colour="#ff5108"},
            new ActivityType() {Text="Decline Offer", Value=13, Colour="#ea2f47"},
            new ActivityType() {Text="Rejected", Value=14, Colour="#e31732"},
            new ActivityType() {Text="Send Thank You", Value=15, Colour="#a21024"},
            new ActivityType() {Text="Email", Value=16, Colour="#f8bac2"},
            new ActivityType() {Text="Meeting", Value=17, Colour="#ff7ce4"},
            new ActivityType() {Text="Phone Call", Value=18, Colour="#8c61ff"},
            new ActivityType() {Text="Send Availability", Value=19, Colour="#5f59f7"},
            new ActivityType() {Text="Assignment", Value=20, Colour="#343090"},
            new ActivityType() {Text="Networking Event", Value=21, Colour="#0047d1"},
            new ActivityType() {Text="Application Withdrawn", Value=22, Colour="#002b80"},
            new ActivityType() {Text="Other", Value=23, Colour="#001a4d"},
        };
    }
}