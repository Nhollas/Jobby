using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jobby.Client.ViewModels.ActivityViewModels;

public class CreateActivityViewModel
{
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public Guid JobId { get; set; }
    public Guid BoardId { get; set; }


    // Additional View Model Properties.
    public int ActivityTypesOption { get; set; }
    public List<SelectListItem> ActivityTypes { get; set; }

    public void OnGet()
    {
        Dictionary<int, List<SelectListItem>> activityTypesDict = new()
        {
            {1, new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Apply", Value="1"}
                }
            },
            {2, new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Phone Screen", Value="2"},
                    new SelectListItem() {Text="Phone Interview", Value="3"},
                    new SelectListItem() {Text="On Site Interview", Value="4"}
                }
            },
            {3, new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Offer Received", Value="5"}
                }
            },
            {4, new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Accept Offer", Value="6"}
                }
            },
            {5, new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Apply", Value="1"},
                    new SelectListItem() {Text="Phone Screen", Value="2"},
                    new SelectListItem() {Text="Phone Interview", Value="3"},
                    new SelectListItem() {Text="On Site Interview", Value="4"},
                    new SelectListItem() {Text="Offer Received", Value="5"},
                    new SelectListItem() {Text="Accept Offer", Value="6"},
                    new SelectListItem() {Text="Prep Cover Letter", Value="7"},
                    new SelectListItem() {Text="Prep Resume", Value="8"},
                    new SelectListItem() {Text="Reach Out", Value="9"},
                    new SelectListItem() {Text="Get Reference", Value="10"},
                    new SelectListItem() {Text="Follow Up", Value="11"},
                    new SelectListItem() {Text="Prep For Interview", Value="12"},
                    new SelectListItem() {Text="Decline Offer", Value="13"},
                    new SelectListItem() {Text="Rejected", Value="14"},
                    new SelectListItem() {Text="Send Thank You", Value="15"},
                    new SelectListItem() {Text="Email", Value="16"},
                    new SelectListItem() {Text="Meeting", Value="17"},
                    new SelectListItem() {Text="Phone Call", Value="18"},
                    new SelectListItem() {Text="Send Availability", Value="19"},
                    new SelectListItem() {Text="Assignment", Value="20"},
                    new SelectListItem() {Text="Networking Event", Value="21"},
                    new SelectListItem() {Text="Application Withdrawn", Value="22"},
                    new SelectListItem() {Text="Other", Value="23"},
                }
            },
        };

        activityTypesDict.TryGetValue(ActivityTypesOption, out var categoryTypes);

        ActivityTypes = categoryTypes;
        ActivityType = int.Parse(ActivityTypes[0].Value);
    }
}
