// Package httpapi serves the Jobby JSON API over HTTP.
package httpapi

import (
	"context"
	"log/slog"
	"net/http"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
)

// Store is the persistence the server depends on. *postgres.Store implements it.
type Store interface {
	BoardWithJobs(ctx context.Context, reference string) (*jobby.Board, error)
	BoardActivities(ctx context.Context, boardReference string) ([]*jobby.Activity, error)
	InsertActivity(ctx context.Context, activity *jobby.Activity) error
	InsertBoard(ctx context.Context, board *jobby.Board) error
}

// Verifier authenticates a bearer token and returns the ID of the user it was
// issued to. *auth.ClerkVerifier implements it.
type Verifier interface {
	Verify(token string) (userID string, err error)
}

// Server is an http.Handler that serves the API. Every route requires a valid
// bearer token.
type Server struct {
	store    Store
	verifier Verifier
	clock    jobby.Clock
	logger   *slog.Logger
	handler  http.Handler
}

// NewServer returns a Server that stores data in store, authenticates requests
// with verifier, timestamps new resources with clock, and logs to logger.
func NewServer(store Store, verifier Verifier, clock jobby.Clock, logger *slog.Logger) *Server {
	s := &Server{store: store, verifier: verifier, clock: clock, logger: logger}

	mux := http.NewServeMux()
	mux.HandleFunc("POST /activity", s.handleCreateActivity)
	mux.HandleFunc("POST /board", s.handleCreateBoard)
	mux.HandleFunc("GET /board/{reference}", s.handleGetBoard)

	// Logging is outermost so that every request is logged, including those
	// rejected by requireUser and those that panic.
	s.handler = s.logRequests(s.recoverPanic(s.requireUser(mux)))
	return s
}

// ServeHTTP implements http.Handler.
func (s *Server) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	s.handler.ServeHTTP(w, r)
}
