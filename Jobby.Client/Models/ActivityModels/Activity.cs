using Jobby.Client.Models.Common;

namespace Jobby.Client.Models.ActivityModels;

public class Activity : BaseEntity
{
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public string ActivityName => activityNamesDict[ActivityType];
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    private readonly Dictionary<int, string> activityNamesDict = new()
    {
        {1, "Apply"},
        {2, "Phone Screen"},
        {3, "Phone Interview"},
        {4, "On Site Interview"},
        {5, "Offer Received"},
        {6, "Accept Offer"},
        {7, "Prep Cover Letter"},
        {8, "Prep Resume"},
        {9, "Reach Out"},
        {10, "Get Reference"},
        {11, "Follow Up"},
        {12, "Prep For Interview"},
        {13, "Decline Offer"},
        {14, "Rejected"},
        {15, "Send Thank You"},
        {16, "Email"},
        {17, "Meeting"},
        {18, "Phone Call"},
        {19, "Send Availability"},
        {20, "Assignment"},
        {21, "Networking Event"},
        {22, "Application Withdrawn"},
        {23, "Other"},
    };
}
