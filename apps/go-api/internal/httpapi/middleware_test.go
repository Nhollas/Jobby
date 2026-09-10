package httpapi

import (
	"bytes"
	"errors"
	"io"
	"log/slog"
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"
)

// verifierFunc adapts a function to the Verifier interface.
type verifierFunc func(token string) (string, error)

func (f verifierFunc) Verify(token string) (string, error) { return f(token) }

func newTestServer(logs io.Writer, verifier Verifier) *Server {
	return &Server{verifier: verifier, logger: slog.New(slog.NewJSONHandler(logs, nil))}
}

func TestRequireUser(t *testing.T) {
	verifier := verifierFunc(func(token string) (string, error) {
		if token != "good" {
			return "", errors.New("bad token")
		}
		return "user_1", nil
	})

	tests := []struct {
		name          string
		authorization string
		wantStatus    int
	}{
		{"valid token", "Bearer good", http.StatusOK},
		{"lowercase scheme", "bearer good", http.StatusOK},
		{"extra space before token", "Bearer  good", http.StatusOK},
		{"no header", "", http.StatusUnauthorized},
		{"other scheme", "Basic good", http.StatusUnauthorized},
		{"scheme only", "Bearer", http.StatusUnauthorized},
		{"invalid token", "Bearer bad", http.StatusUnauthorized},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			var gotUser string
			next := http.HandlerFunc(func(_ http.ResponseWriter, r *http.Request) {
				userID, err := currentUser(r)
				if err != nil {
					t.Errorf("currentUser() error = %v", err)
				}
				gotUser = userID
			})
			h := newTestServer(io.Discard, verifier).requireUser(next)

			req := httptest.NewRequestWithContext(t.Context(), http.MethodGet, "/", nil)
			if tt.authorization != "" {
				req.Header.Set("Authorization", tt.authorization)
			}
			rec := httptest.NewRecorder()
			h.ServeHTTP(rec, req)

			if rec.Code != tt.wantStatus {
				t.Fatalf("status = %d, want %d", rec.Code, tt.wantStatus)
			}
			if tt.wantStatus == http.StatusUnauthorized {
				if got := rec.Header().Get("WWW-Authenticate"); got != "Bearer" {
					t.Errorf("WWW-Authenticate = %q, want %q", got, "Bearer")
				}
				return
			}
			if gotUser != "user_1" {
				t.Errorf("user = %q, want %q", gotUser, "user_1")
			}
		})
	}
}

func TestRecoverPanicResponds500(t *testing.T) {
	var logs bytes.Buffer
	h := newTestServer(&logs, nil).recoverPanic(http.HandlerFunc(func(http.ResponseWriter, *http.Request) {
		panic("boom")
	}))

	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, httptest.NewRequestWithContext(t.Context(), http.MethodGet, "/", nil))

	if rec.Code != http.StatusInternalServerError {
		t.Errorf("status = %d, want %d", rec.Code, http.StatusInternalServerError)
	}
	if got := rec.Header().Get("Content-Type"); got != "application/json" {
		t.Errorf("Content-Type = %q, want %q", got, "application/json")
	}
	if !strings.Contains(logs.String(), `"stack"`) {
		t.Errorf("panic log has no stack trace: %s", logs.String())
	}
}

func TestRecoverPanicAbortsStartedResponse(t *testing.T) {
	h := newTestServer(io.Discard, nil).recoverPanic(http.HandlerFunc(func(w http.ResponseWriter, _ *http.Request) {
		w.WriteHeader(http.StatusOK)
		panic("boom")
	}))

	defer func() {
		got := recover()
		if err, _ := got.(error); !errors.Is(err, http.ErrAbortHandler) {
			t.Errorf("recovered %v, want http.ErrAbortHandler", got)
		}
	}()
	h.ServeHTTP(httptest.NewRecorder(), httptest.NewRequestWithContext(t.Context(), http.MethodGet, "/", nil))
}

func TestRecoverPanicPassesThroughAbortHandler(t *testing.T) {
	var logs bytes.Buffer
	h := newTestServer(&logs, nil).recoverPanic(http.HandlerFunc(func(http.ResponseWriter, *http.Request) {
		panic(http.ErrAbortHandler)
	}))

	defer func() {
		got := recover()
		if err, _ := got.(error); !errors.Is(err, http.ErrAbortHandler) {
			t.Errorf("recovered %v, want http.ErrAbortHandler", got)
		}
		if logs.Len() != 0 {
			t.Errorf("an intentional abort was logged: %s", logs.String())
		}
	}()
	h.ServeHTTP(httptest.NewRecorder(), httptest.NewRequestWithContext(t.Context(), http.MethodGet, "/", nil))
}

func TestLogRequestsRecordsStatus(t *testing.T) {
	var logs bytes.Buffer
	h := newTestServer(&logs, nil).logRequests(http.HandlerFunc(func(w http.ResponseWriter, _ *http.Request) {
		w.WriteHeader(http.StatusTeapot)
	}))

	h.ServeHTTP(httptest.NewRecorder(), httptest.NewRequestWithContext(t.Context(), http.MethodGet, "/", nil))

	if !strings.Contains(logs.String(), `"status":418`) {
		t.Errorf("log does not record status 418: %s", logs.String())
	}
}

func TestLogRequestsPreservesFlush(t *testing.T) {
	var flushErr error
	h := newTestServer(io.Discard, nil).logRequests(http.HandlerFunc(func(w http.ResponseWriter, _ *http.Request) {
		flushErr = http.NewResponseController(w).Flush()
	}))

	rec := httptest.NewRecorder()
	h.ServeHTTP(rec, httptest.NewRequestWithContext(t.Context(), http.MethodGet, "/", nil))

	if flushErr != nil {
		t.Fatalf("Flush() through logRequests error = %v", flushErr)
	}
	if !rec.Flushed {
		t.Error("underlying ResponseWriter was not flushed")
	}
}
