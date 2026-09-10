package httpapi

import (
	"net/http"
	"time"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
)

// createActivityRequest is the body of POST /activity.
type createActivityRequest struct {
	BoardReference string             `json:"boardReference"`
	Title          string             `json:"title"`
	Type           jobby.ActivityType `json:"type"`
	StartDate      time.Time          `json:"startDate"`
	EndDate        time.Time          `json:"endDate"`
	Note           string             `json:"note"`
	Completed      bool               `json:"completed"`
	JobReference   string             `json:"jobReference,omitempty"`
}

// Valid implements validator.
func (r createActivityRequest) Valid() map[string]string {
	problems := make(map[string]string)
	if r.Title == "" {
		problems["title"] = "This property is required."
	}
	if r.BoardReference == "" {
		problems["boardReference"] = "This property is required."
	}
	if !r.Type.Valid() {
		problems["type"] = "The provided activity type is not valid."
	}
	if r.StartDate.IsZero() {
		problems["startDate"] = "This property is required."
	}
	if !r.EndDate.IsZero() && r.EndDate.Before(r.StartDate) {
		problems["endDate"] = "The end date must not be before the start date."
	}
	return problems
}

// handleCreateActivity handles POST /activity. It responds:
//   - 201 with the new activity;
//   - 400 if the body is malformed or the job reference is not on the board;
//   - 404 if the board does not exist or belongs to another user;
//   - 413 if the body is too large;
//   - 422 if the body fails validation.
func (s *Server) handleCreateActivity(w http.ResponseWriter, r *http.Request) {
	req, problems, err := decodeValid[createActivityRequest](w, r)
	if err != nil {
		s.failDecode(w, r, err)
		return
	}
	if len(problems) > 0 {
		s.failValidation(w, r, problems)
		return
	}

	userID, err := currentUser(r)
	if err != nil {
		s.fail(w, r, err)
		return
	}

	ctx := r.Context()
	board, err := s.store.BoardWithJobs(ctx, req.BoardReference)
	if err != nil {
		s.fail(w, r, err)
		return
	}
	if !board.IsOwnedBy(userID) {
		s.fail(w, r, &jobby.NotOwnedError{Reference: board.Reference})
		return
	}

	activity, err := board.NewActivity(s.clock, jobby.ActivityParams{
		Title:        req.Title,
		Type:         req.Type,
		StartDate:    req.StartDate,
		EndDate:      req.EndDate,
		Note:         req.Note,
		Completed:    req.Completed,
		JobReference: req.JobReference,
	})
	if err != nil {
		s.fail(w, r, err)
		return
	}

	if err := s.store.InsertActivity(ctx, activity); err != nil {
		s.fail(w, r, err)
		return
	}
	s.respond(w, r, http.StatusCreated, toActivityDTO(activity))
}
