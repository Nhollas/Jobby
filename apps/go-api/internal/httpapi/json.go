package httpapi

import (
	"bytes"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"log/slog"
	"net/http"
)

// maxRequestBytes is the largest request body the API accepts.
const maxRequestBytes = 1 << 20

// validator is implemented by request body types.
type validator interface {
	// Valid returns a message for each invalid field, keyed by JSON field
	// name. An empty map means the value is valid.
	Valid() map[string]string
}

// errTrailingData is returned by decodeValid when the request body contains
// more than one JSON value.
var errTrailingData = errors.New("request body must contain a single JSON value")

// decodeValid decodes the request body into a T and validates it.
//
// The body must be one JSON value of at most maxRequestBytes, with no fields
// that T does not declare. As elsewhere in encoding/json, field names match
// case-insensitively. A non-nil error means the body could not be decoded;
// otherwise problems holds the result of T's Valid method.
func decodeValid[T validator](w http.ResponseWriter, r *http.Request) (T, map[string]string, error) {
	var v T
	dec := json.NewDecoder(http.MaxBytesReader(w, r.Body, maxRequestBytes))
	dec.DisallowUnknownFields()

	if err := dec.Decode(&v); err != nil {
		return v, nil, fmt.Errorf("decode request body: %w", err)
	}
	if err := dec.Decode(&struct{}{}); !errors.Is(err, io.EOF) {
		var tooLarge *http.MaxBytesError
		if errors.As(err, &tooLarge) {
			return v, nil, fmt.Errorf("decode request body: %w", err)
		}
		return v, nil, errTrailingData
	}
	return v, v.Valid(), nil
}

// apiMessage is the body of every error response except validation failures.
type apiMessage struct {
	Message string `json:"message"`
}

// validationProblem is the body of a 422 response.
type validationProblem struct {
	Message string            `json:"message"`
	Errors  map[string]string `json:"errors"`
}

// respond writes body as JSON with the given status.
func (s *Server) respond(w http.ResponseWriter, r *http.Request, status int, body any) {
	// Encode before writing the status, so that an encoding failure can still
	// be reported as a 500 rather than as a truncated response.
	var buf bytes.Buffer
	if err := json.NewEncoder(&buf).Encode(body); err != nil {
		s.logger.ErrorContext(r.Context(), "encode response", slog.Any("error", err))
		s.respond(w, r, http.StatusInternalServerError, apiMessage{Message: "something went wrong"})
		return
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	if _, err := buf.WriteTo(w); err != nil {
		// Almost always the client going away. Nothing more can be sent.
		s.logger.DebugContext(r.Context(), "write response", slog.Any("error", err))
	}
}
