package httpapi

import (
	"net/http"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
	"golang.org/x/sync/errgroup"
)

// createBoardRequest is the body of POST /board.
type createBoardRequest struct {
	Name string `json:"name"`
}

// Valid implements validator.
func (r createBoardRequest) Valid() map[string]string {
	problems := make(map[string]string)
	if r.Name == "" {
		problems["name"] = "This property is required."
	}
	return problems
}

// handleCreateBoard handles POST /board. It responds 201 with the new board,
// which has the default lists; 400 or 413 if the body cannot be decoded; and
// 422 if it fails validation.
func (s *Server) handleCreateBoard(w http.ResponseWriter, r *http.Request) {
	req, problems, err := decodeValid[createBoardRequest](w, r)
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

	board := jobby.NewBoard(s.clock, userID, req.Name)
	if err := s.store.InsertBoard(r.Context(), board); err != nil {
		s.fail(w, r, err)
		return
	}
	s.respond(w, r, http.StatusCreated, toBoardDTO(board, nil))
}

// handleGetBoard handles GET /board/{reference}. It responds 200 with the
// board, its lists and jobs, and its activities, or 404 if the board does not
// exist or belongs to another user.
func (s *Server) handleGetBoard(w http.ResponseWriter, r *http.Request) {
	userID, err := currentUser(r)
	if err != nil {
		s.fail(w, r, err)
		return
	}
	reference := r.PathValue("reference")

	// The board and its activities are independent queries, so they run
	// concurrently. If either fails, the group's context cancels the other.
	g, ctx := errgroup.WithContext(r.Context())
	var (
		board      *jobby.Board
		activities []*jobby.Activity
	)
	g.Go(func() (err error) {
		board, err = s.store.BoardWithJobs(ctx, reference)
		return err
	})
	g.Go(func() (err error) {
		activities, err = s.store.BoardActivities(ctx, reference)
		return err
	})
	if err := g.Wait(); err != nil {
		s.fail(w, r, err)
		return
	}

	if !board.IsOwnedBy(userID) {
		s.fail(w, r, &jobby.NotOwnedError{Reference: board.Reference})
		return
	}
	s.respond(w, r, http.StatusOK, toBoardDTO(board, activities))
}
