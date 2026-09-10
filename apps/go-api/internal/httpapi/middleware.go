package httpapi

import (
	"errors"
	"log/slog"
	"net/http"
	"runtime/debug"
	"strings"
	"time"

	"github.com/Nhollas/Jobby/apps/go-api/internal/auth"
)

// requireUser responds 401 to a request without a valid bearer token, and
// otherwise passes the request on with the token's user ID in its context.
func (s *Server) requireUser(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		// Authentication scheme names are case-insensitive (RFC 9110 section 11.1).
		scheme, token, ok := strings.Cut(r.Header.Get("Authorization"), " ")
		token = strings.TrimSpace(token)
		if !ok || !strings.EqualFold(scheme, "Bearer") || token == "" {
			s.unauthorized(w, r)
			return
		}

		userID, err := s.verifier.Verify(token)
		if err != nil {
			s.logger.DebugContext(r.Context(), "token rejected", slog.Any("error", err))
			s.unauthorized(w, r)
			return
		}
		next.ServeHTTP(w, r.WithContext(auth.WithUser(r.Context(), userID)))
	})
}

// unauthorized responds 401 with the WWW-Authenticate header that RFC 9110
// requires on every 401 response.
func (s *Server) unauthorized(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("WWW-Authenticate", "Bearer")
	s.respond(w, r, http.StatusUnauthorized, apiMessage{Message: "unauthenticated"})
}

// currentUser returns the ID of the user who made r. requireUser guarantees
// there is one, so its absence is a server bug and is reported as an error.
func currentUser(r *http.Request) (string, error) {
	userID, ok := auth.UserFromContext(r.Context())
	if !ok {
		return "", errors.New("no authenticated user in request context")
	}
	return userID, nil
}

// logRequests logs the method, path, status and duration of each request.
func (s *Server) logRequests(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		start := time.Now()
		rec := &responseRecorder{ResponseWriter: w, status: http.StatusOK}

		next.ServeHTTP(rec, r)

		s.logger.InfoContext(r.Context(), "request",
			slog.String("method", r.Method),
			slog.String("path", r.URL.Path),
			slog.Int("status", rec.status),
			slog.Duration("duration", time.Since(start)),
		)
	})
}

// recoverPanic turns a panic in a handler into a 500 response and an error log
// that includes the stack trace. If the handler had already started its
// response, the response is aborted instead, since a 500 can no longer be sent.
// http.ErrAbortHandler is passed through for net/http to handle as documented.
func (s *Server) recoverPanic(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		rec := &responseRecorder{ResponseWriter: w, status: http.StatusOK}

		defer func() {
			v := recover()
			if v == nil {
				return
			}
			if err, ok := v.(error); ok && errors.Is(err, http.ErrAbortHandler) {
				panic(v)
			}

			s.logger.ErrorContext(r.Context(), "panic serving request",
				slog.String("method", r.Method),
				slog.String("path", r.URL.Path),
				slog.Any("panic", v),
				slog.String("stack", string(debug.Stack())),
			)
			if rec.wroteHeader {
				panic(http.ErrAbortHandler)
			}
			s.respond(w, r, http.StatusInternalServerError, apiMessage{Message: "something went wrong"})
		}()

		next.ServeHTTP(rec, r)
	})
}

// responseRecorder is an http.ResponseWriter that records the status of the
// response written through it.
type responseRecorder struct {
	http.ResponseWriter
	status      int
	wroteHeader bool
}

// WriteHeader records status and passes it on. Informational (1xx) statuses
// may precede the final one, so they are not recorded.
func (r *responseRecorder) WriteHeader(status int) {
	if !r.wroteHeader && status >= http.StatusOK {
		r.status = status
		r.wroteHeader = true
	}
	r.ResponseWriter.WriteHeader(status)
}

// Write passes b on, first recording the implicit 200 status if no status has
// been written.
func (r *responseRecorder) Write(b []byte) (int, error) {
	if !r.wroteHeader {
		r.WriteHeader(http.StatusOK)
	}
	return r.ResponseWriter.Write(b)
}

// Unwrap returns the underlying ResponseWriter, which lets
// http.ResponseController reach its optional methods, such as Flush.
func (r *responseRecorder) Unwrap() http.ResponseWriter {
	return r.ResponseWriter
}
