package jobby

import "fmt"

// NotFoundError reports that no resource has the given reference.
type NotFoundError struct {
	Reference string
}

// Error implements the error interface.
func (e *NotFoundError) Error() string {
	return fmt.Sprintf("no resource with reference %q", e.Reference)
}

// NotOwnedError reports that a resource exists but belongs to another user.
// It carries only the reference, never the owner's ID.
type NotOwnedError struct {
	Reference string
}

// Error implements the error interface.
func (e *NotOwnedError) Error() string {
	return fmt.Sprintf("resource %q is not owned by this user", e.Reference)
}

// JobNotOnBoardError reports an attempt to link an activity to a job that is
// not on the activity's board.
type JobNotOnBoardError struct {
	JobReference   string
	BoardReference string
}

// Error implements the error interface.
func (e *JobNotOnBoardError) Error() string {
	return fmt.Sprintf("job %q is not on board %q", e.JobReference, e.BoardReference)
}
