package jobby

import (
	"iter"
	"time"
)

// Board is a user's job search: an ordered set of lists that hold jobs.
// Lists, jobs and activities are created through a Board so that they always
// belong to it.
type Board struct {
	Reference   string
	OwnerID     string
	Name        string
	CreatedDate time.Time
	LastUpdated time.Time
	Lists       []*JobList
}

// JobList is a column on a board, such as "Applied" or "Interview".
type JobList struct {
	Reference      string
	OwnerID        string
	BoardReference string
	Name           string
	Position       int
	CreatedDate    time.Time
	LastUpdated    time.Time
	Jobs           []*Job
}

// Job is a job opening the user is tracking.
type Job struct {
	Reference        string
	OwnerID          string
	BoardReference   string
	JobListReference string
	Company          string
	Title            string
	Position         int
	CreatedDate      time.Time
	LastUpdated      time.Time
}

// defaultListNames are the lists every new board starts with, in order.
var defaultListNames = [...]string{"Applied", "Wishlist", "Interview", "Offer", "Rejected"}

// NewBoard returns a board named name and owned by ownerID, with the lists
// Applied, Wishlist, Interview, Offer and Rejected.
func NewBoard(clock Clock, ownerID, name string) *Board {
	now := clock.Now()
	board := &Board{
		Reference:   newReference(boardPrefix),
		OwnerID:     ownerID,
		Name:        name,
		CreatedDate: now,
		LastUpdated: now,
	}
	for _, listName := range defaultListNames {
		board.AddList(clock, listName)
	}
	return board
}

// IsOwnedBy reports whether userID owns b.
func (b *Board) IsOwnedBy(userID string) bool { return b.OwnerID == userID }

// AddList appends a list named name to b and returns it.
func (b *Board) AddList(clock Clock, name string) *JobList {
	now := clock.Now()
	list := &JobList{
		Reference:      newReference(jobListPrefix),
		OwnerID:        b.OwnerID,
		BoardReference: b.Reference,
		Name:           name,
		Position:       len(b.Lists) + 1,
		CreatedDate:    now,
		LastUpdated:    now,
	}
	b.Lists = append(b.Lists, list)
	return list
}

// AddJob appends a job with the given company and title to l and returns it.
func (l *JobList) AddJob(clock Clock, company, title string) *Job {
	now := clock.Now()
	job := &Job{
		Reference:        newReference(jobPrefix),
		OwnerID:          l.OwnerID,
		BoardReference:   l.BoardReference,
		JobListReference: l.Reference,
		Company:          company,
		Title:            title,
		Position:         len(l.Jobs),
		CreatedDate:      now,
		LastUpdated:      now,
	}
	l.Jobs = append(l.Jobs, job)
	return job
}

// Jobs returns an iterator over the jobs on b, ordered by list and then by
// position within each list.
func (b *Board) Jobs() iter.Seq[*Job] {
	return func(yield func(*Job) bool) {
		for _, list := range b.Lists {
			for _, job := range list.Jobs {
				if !yield(job) {
					return
				}
			}
		}
	}
}

// HasJob reports whether the job with the given reference is on b.
func (b *Board) HasJob(reference string) bool {
	for job := range b.Jobs() {
		if job.Reference == reference {
			return true
		}
	}
	return false
}

// ActivityParams are the caller-supplied fields of a new Activity.
type ActivityParams struct {
	Title     string
	Type      ActivityType
	StartDate time.Time
	EndDate   time.Time
	Note      string
	Completed bool
	// JobReference, if not empty, links the activity to that job, which must
	// be on the board.
	JobReference string
}

// NewActivity returns a new activity on b. It returns a *JobNotOnBoardError if
// p.JobReference names a job that is not on b. The activity is not recorded on
// b; the caller is responsible for storing it.
func (b *Board) NewActivity(clock Clock, p ActivityParams) (*Activity, error) {
	if p.JobReference != "" && !b.HasJob(p.JobReference) {
		return nil, &JobNotOnBoardError{
			JobReference:   p.JobReference,
			BoardReference: b.Reference,
		}
	}

	now := clock.Now()
	return &Activity{
		Reference:      newReference(activityPrefix),
		OwnerID:        b.OwnerID,
		BoardReference: b.Reference,
		JobReference:   p.JobReference,
		Title:          p.Title,
		Type:           p.Type,
		StartDate:      p.StartDate,
		EndDate:        p.EndDate,
		Note:           p.Note,
		Completed:      p.Completed,
		CreatedDate:    now,
		LastUpdated:    now,
	}, nil
}
