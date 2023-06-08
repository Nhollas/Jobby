namespace Jobby.Domain.Static;

public static class ActivityConstants
{
    public static readonly Dictionary<int, string> Types = new()
    {
        {0, "Apply"},
        {1, "Phone Screen"},
        {2, "Phone Interview"},
        {3, "On Site Interview"},
        {4, "Offer Received"},
        {5, "Accept Offer"},
        {6, "Prep Cover Letter"},
        {7, "Prep Resume"},
        {8, "Reach Out"},
        {9, "Get Reference"},
        {10, "Follow Up"},
        {11, "Prep For Interview"},
        {12, "Decline Offer"},
        {13, "Rejected"},
        {14, "Send Thank You"},
        {15, "Email"},
        {16, "Meeting"},
        {17, "Phone Call"},
        {18, "Send Availability"},
        {19, "Assignment"},
        {20, "Networking Event"},
        {21, "Application Withdrawn"},
        {22, "Other"}
    };
}
