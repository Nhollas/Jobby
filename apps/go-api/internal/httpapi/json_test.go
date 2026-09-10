package httpapi

import (
	"errors"
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"
)

type testBody struct {
	Name string `json:"name"`
}

func (b testBody) Valid() map[string]string {
	if b.Name == "" {
		return map[string]string{"name": "required"}
	}
	return nil
}

func TestDecodeValid(t *testing.T) {
	tests := []struct {
		name         string
		body         string
		wantErr      bool
		wantTooLarge bool
		wantProblems int
	}{
		{name: "valid", body: `{"name":"a"}`},
		{name: "trailing whitespace", body: "{\"name\":\"a\"}\n"},
		{name: "invalid", body: `{"name":""}`, wantProblems: 1},
		{name: "malformed", body: `{"name":`, wantErr: true},
		{name: "empty", body: ``, wantErr: true},
		{name: "unknown field", body: `{"name":"a","extra":1}`, wantErr: true},
		{name: "trailing garbage", body: `{"name":"a"} junk`, wantErr: true},
		{name: "second value", body: `{"name":"a"}{"name":"b"}`, wantErr: true},
		{
			name:         "too large",
			body:         `{"name":"` + strings.Repeat("a", maxRequestBytes) + `"}`,
			wantErr:      true,
			wantTooLarge: true,
		},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			req := httptest.NewRequestWithContext(t.Context(), http.MethodPost, "/", strings.NewReader(tt.body))

			_, problems, err := decodeValid[testBody](httptest.NewRecorder(), req)

			if gotErr := err != nil; gotErr != tt.wantErr {
				t.Fatalf("decodeValid() error = %v, wantErr %v", err, tt.wantErr)
			}
			var tooLarge *http.MaxBytesError
			if got := errors.As(err, &tooLarge); got != tt.wantTooLarge {
				t.Errorf("error is *http.MaxBytesError = %v, want %v (error: %v)", got, tt.wantTooLarge, err)
			}
			if len(problems) != tt.wantProblems {
				t.Errorf("got %d problems, want %d: %v", len(problems), tt.wantProblems, problems)
			}
		})
	}
}
