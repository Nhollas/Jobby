package httpapi

import (
	"context"
	"errors"
	"fmt"
	"log/slog"
	"net/http"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
)

// fail responds to err with a status and message chosen by the error's type,
// and logs it if the status is 5xx. Errors of any other type become a 500 with
// a generic message, so internal details never reach the client.
func (s *Server) fail(w http.ResponseWriter, r *http.Request, err error) {
	// The client has gone; there is no one to respond to.
	if errors.Is(err, context.Canceled) && r.Context().Err() != nil {
		return
	}

	status, message := http.StatusInternalServerError, "something went wrong"

	var (
		notFound   *jobby.NotFoundError
		notOwned   *jobby.NotOwnedError
		notOnBoard *jobby.JobNotOnBoardError
	)
	switch {
	case errors.As(err, &notFound):
		status, message = http.StatusNotFound, notFound.Error()
	case errors.As(err, &notOwned):
		// Respond exactly as for a missing resource, so that references
		// belonging to other users cannot be discovered by probing.
		// RFC 9110 section 15.5.4 permits 404 in place of 403 for this.
		notFound := &jobby.NotFoundError{Reference: notOwned.Reference}
		status, message = http.StatusNotFound, notFound.Error()
	case errors.As(err, &notOnBoard):
		status, message = http.StatusBadRequest, notOnBoard.Error()
	}

	if status >= http.StatusInternalServerError {
		s.logger.ErrorContext(r.Context(), "request failed",
			slog.String("method", r.Method),
			slog.String("path", r.URL.Path),
			slog.Any("error", err),
		)
	}
	s.respond(w, r, status, apiMessage{Message: message})
}

// failDecode responds to an error from decodeValid: 413 if the body was too
// large, and 400 otherwise.
func (s *Server) failDecode(w http.ResponseWriter, r *http.Request, err error) {
	var tooLarge *http.MaxBytesError
	if errors.As(err, &tooLarge) {
		s.respond(w, r, http.StatusRequestEntityTooLarge, apiMessage{
			Message: fmt.Sprintf("request body must not exceed %d bytes", tooLarge.Limit),
		})
		return
	}
	s.respond(w, r, http.StatusBadRequest, apiMessage{Message: "malformed request body"})
}

// failValidation responds 422 with the given field problems.
func (s *Server) failValidation(w http.ResponseWriter, r *http.Request, problems map[string]string) {
	s.respond(w, r, http.StatusUnprocessableEntity, validationProblem{
		Message: "One or more validation errors occurred.",
		Errors:  problems,
	})
}
