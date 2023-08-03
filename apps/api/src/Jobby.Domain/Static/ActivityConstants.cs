namespace Jobby.Domain.Static;

public static class ActivityConstants
{
    public enum Types
    {
        Apply = 0,
        PhoneScreen = 1,
        PhoneInterview = 2,
        OnSiteInterview = 3,
        OfferReceived = 4,
        AcceptOffer = 5,
        PrepCoverLetter = 6,
        PrepResume = 7,
        ReachOut = 8,
        GetReference = 9,
        FollowUp = 10,
        PrepForInterview = 11,
        DeclineOffer = 12,
        Rejected = 13,
        SendThankYou = 14,
        Email = 15,
        Meeting = 16,
        PhoneCall = 17,
        SendAvailability = 18,
        Assignment = 19,
        NetworkingEvent = 20,
        ApplicationWithdrawn = 21,
        Other = 22
    }
    
    public static readonly Dictionary<int, string> TypesDictionary = new()
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
