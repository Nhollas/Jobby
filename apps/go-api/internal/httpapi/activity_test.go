package httpapi_test

import (
	"net/http"
	"strings"
	"testing"
	"time"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
	"github.com/Nhollas/Jobby/apps/go-api/internal/testenv"
)

// body is a JSON request body. It is a map rather than the handler's request
// type, so that the tests use the field names a client actually sends.
type body map[string]any

func validActivity(boardRef string) body {
	return body{
		"boardReference": boardRef,
		"title":          "Test Activity",
		"type":           int(jobby.PhoneScreen),
		"startDate":      testenv.Now,
		"endDate":        testenv.Now.Add(24 * time.Hour),
		"note":           "Test Note",
		"completed":      false,
	}
}

func TestCreateActivity(t *testing.T) {
	env := testenv.New(t)

	tests := []struct {
		name string
		body func(t *testing.T) body
		// token returns the bearer token to send. If nil, a valid token for
		// testenv.TestUserID is sent.
		token      func(t *testing.T) string
		wantStatus int
		// wantErrors lists the fields expected in a 422 response.
		wantErrors []string
	}{
		{
			name: "with job reference",
			body: func(t *testing.T) body {
				board := env.SeedBoard(t, testenv.TestUserID, 1)
				b := validActivity(board.Reference)
				b["jobReference"] = board.Lists[0].Jobs[0].Reference
				return b
			},
			wantStatus: http.StatusCreated,
		},
		{
			name: "without job reference",
			body: func(t *testing.T) body {
				return validActivity(env.SeedBoard(t, testenv.TestUserID, 0).Reference)
			},
			wantStatus: http.StatusCreated,
		},
		{
			name:       "with unknown board",
			body:       func(*testing.T) body { return validActivity("bo_unknown") },
			wantStatus: http.StatusNotFound,
		},
		{
			name: "with unknown job",
			body: func(t *testing.T) body {
				b := validActivity(env.SeedBoard(t, testenv.TestUserID, 1).Reference)
				b["jobReference"] = "jo_unknown"
				return b
			},
			wantStatus: http.StatusBadRequest,
		},
		{
			name: "with job on another board",
			body: func(t *testing.T) body {
				mine := env.SeedBoard(t, testenv.TestUserID, 0)
				other := env.SeedBoard(t, testenv.TestUserID, 1)
				b := validActivity(mine.Reference)
				b["jobReference"] = other.Lists[0].Jobs[0].Reference
				return b
			},
			wantStatus: http.StatusBadRequest,
		},
		{
			name: "with another user's board",
			body: func(t *testing.T) body {
				return validActivity(env.SeedBoard(t, testenv.OtherUserID, 0).Reference)
			},
			wantStatus: http.StatusNotFound,
		},
		{
			name:       "with validation errors",
			body:       func(*testing.T) body { return body{"title": "", "type": 999, "completed": false} },
			wantStatus: http.StatusUnprocessableEntity,
			wantErrors: []string{"title", "boardReference", "type", "startDate"},
		},
		{
			name: "with end date before start date",
			body: func(t *testing.T) body {
				b := validActivity(env.SeedBoard(t, testenv.TestUserID, 0).Reference)
				b["endDate"] = testenv.Now.Add(-24 * time.Hour)
				return b
			},
			wantStatus: http.StatusUnprocessableEntity,
			wantErrors: []string{"endDate"},
		},
		{
			name:       "without token",
			body:       func(*testing.T) body { return validActivity("bo_any") },
			token:      func(*testing.T) string { return "" },
			wantStatus: http.StatusUnauthorized,
		},
		{
			name:       "with expired token",
			body:       func(*testing.T) body { return validActivity("bo_any") },
			token:      func(t *testing.T) string { return env.ExpiredToken(t, testenv.TestUserID) },
			wantStatus: http.StatusUnauthorized,
		},
		{
			name:       "with token signed by an untrusted key",
			body:       func(*testing.T) body { return validActivity("bo_any") },
			token:      func(t *testing.T) string { return env.ForeignToken(t, testenv.TestUserID) },
			wantStatus: http.StatusUnauthorized,
		},
		{
			name: "with token for an unauthorized party",
			body: func(*testing.T) body { return validActivity("bo_any") },
			token: func(t *testing.T) string {
				return env.TokenForParty(t, testenv.TestUserID, "https://attacker.example")
			},
			wantStatus: http.StatusUnauthorized,
		},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			token := env.Token(t, testenv.TestUserID)
			if tt.token != nil {
				token = tt.token(t)
			}

			resp := env.Request(t, http.MethodPost, "/activity", token, tt.body(t))

			if resp.StatusCode != tt.wantStatus {
				t.Fatalf("status = %d, want %d", resp.StatusCode, tt.wantStatus)
			}
			if tt.wantStatus == http.StatusUnauthorized {
				if got := resp.Header.Get("WWW-Authenticate"); got != "Bearer" {
					t.Errorf("WWW-Authenticate = %q, want %q", got, "Bearer")
				}
			}
			if tt.wantErrors == nil {
				return
			}

			problem := testenv.DecodeBody[struct {
				Errors map[string]string `json:"errors"`
			}](t, resp)
			for _, field := range tt.wantErrors {
				if _, ok := problem.Errors[field]; !ok {
					t.Errorf("no validation error for %q; got %v", field, problem.Errors)
				}
			}
			if len(problem.Errors) != len(tt.wantErrors) {
				t.Errorf("got %d validation errors, want %d: %v",
					len(problem.Errors), len(tt.wantErrors), problem.Errors)
			}
		})
	}
}

func TestCreateActivityRejectsUndecodableBodies(t *testing.T) {
	env := testenv.New(t)
	token := env.Token(t, testenv.TestUserID)

	tests := []struct {
		name       string
		body       string
		wantStatus int
	}{
		{"malformed", `{"title":`, http.StatusBadRequest},
		{"unknown field", `{"title":"x","colour":"blue"}`, http.StatusBadRequest},
		{"trailing data", `{"title":"x"} junk`, http.StatusBadRequest},
		// Just over the 1 MiB limit.
		{"too large", `{"note":"` + strings.Repeat("a", 1<<20) + `"}`, http.StatusRequestEntityTooLarge},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			resp := env.RequestRaw(t, http.MethodPost, "/activity", token, []byte(tt.body))

			if resp.StatusCode != tt.wantStatus {
				t.Errorf("status = %d, want %d", resp.StatusCode, tt.wantStatus)
			}
		})
	}
}

func TestCreateActivityStoresActivity(t *testing.T) {
	env := testenv.New(t)
	board := env.SeedBoard(t, testenv.TestUserID, 1)
	job := board.Lists[0].Jobs[0]

	sent := validActivity(board.Reference)
	sent["jobReference"] = job.Reference

	resp := env.Request(t, http.MethodPost, "/activity", env.Token(t, testenv.TestUserID), sent)
	if resp.StatusCode != http.StatusCreated {
		t.Fatalf("status = %d, want %d", resp.StatusCode, http.StatusCreated)
	}

	returned := testenv.DecodeBody[struct {
		Reference    string    `json:"reference"`
		Name         string    `json:"name"`
		JobReference string    `json:"jobReference"`
		CreatedDate  time.Time `json:"createdDate"`
	}](t, resp)

	if returned.Name != "Phone Screen" {
		t.Errorf("name = %q, want %q", returned.Name, "Phone Screen")
	}
	if returned.JobReference != job.Reference {
		t.Errorf("jobReference = %q, want %q", returned.JobReference, job.Reference)
	}
	if !returned.CreatedDate.Equal(testenv.Now) {
		t.Errorf("createdDate = %v, want %v", returned.CreatedDate, testenv.Now)
	}

	stored, err := env.Store.Activity(t.Context(), returned.Reference)
	if err != nil {
		t.Fatalf("Store.Activity(%q) error = %v", returned.Reference, err)
	}
	if stored.Title != "Test Activity" {
		t.Errorf("stored Title = %q, want %q", stored.Title, "Test Activity")
	}
	if stored.Type != jobby.PhoneScreen {
		t.Errorf("stored Type = %v, want %v", stored.Type, jobby.PhoneScreen)
	}
	if stored.JobReference != job.Reference {
		t.Errorf("stored JobReference = %q, want %q", stored.JobReference, job.Reference)
	}
	if stored.OwnerID != testenv.TestUserID {
		t.Errorf("stored OwnerID = %q, want %q", stored.OwnerID, testenv.TestUserID)
	}
	if !stored.CreatedDate.Equal(testenv.Now) {
		t.Errorf("stored CreatedDate = %v, want %v", stored.CreatedDate, testenv.Now)
	}
}
