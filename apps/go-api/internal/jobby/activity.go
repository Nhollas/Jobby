package jobby

import (
	"fmt"
	"time"
)

// ActivityType identifies the kind of an Activity.
type ActivityType int16

// Activity types. The numeric values are stored in the database and exposed by
// the API, so new types must be appended, never inserted or reordered.
const (
	Apply ActivityType = iota
	PhoneScreen
	PhoneInterview
	OnSiteInterview
	OfferReceived
	AcceptOffer
	PrepCoverLetter
	PrepResume
	ReachOut
	GetReference
	FollowUp
	PrepForInterview
	DeclineOffer
	Rejected
	SendThankYou
	Email
	Meeting
	PhoneCall
	SendAvailability
	Assignment
	NetworkingEvent
	ApplicationWithdrawn
	Other
)

// activityTypeNames holds the display name of each ActivityType, indexed by
// its value.
var activityTypeNames = [...]string{
	"Apply", "Phone Screen", "Phone Interview", "On Site Interview",
	"Offer Received", "Accept Offer", "Prep Cover Letter", "Prep Resume",
	"Reach Out", "Get Reference", "Follow Up", "Prep For Interview",
	"Decline Offer", "Rejected", "Send Thank You", "Email", "Meeting",
	"Phone Call", "Send Availability", "Assignment", "Networking Event",
	"Application Withdrawn", "Other",
}

// Valid reports whether t is one of the defined activity types.
func (t ActivityType) Valid() bool {
	return t >= 0 && int(t) < len(activityTypeNames)
}

// String returns the display name of t, such as "Phone Screen", or
// "ActivityType(n)" if t is not valid.
func (t ActivityType) String() string {
	if !t.Valid() {
		return fmt.Sprintf("ActivityType(%d)", int16(t))
	}
	return activityTypeNames[t]
}

// Activity is something the user did or plans to do in their job search, such
// as an interview or a follow-up. It belongs to a board and may be linked to a
// job on that board.
type Activity struct {
	Reference      string
	OwnerID        string
	BoardReference string
	// JobReference is empty if the activity is not linked to a job.
	JobReference string
	Title        string
	Type         ActivityType
	StartDate    time.Time
	EndDate      time.Time
	Note         string
	Completed    bool
	CreatedDate  time.Time
	LastUpdated  time.Time
}
