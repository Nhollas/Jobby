package httpapi_test

import (
	"fmt"
	"net/http"
	"testing"

	"github.com/Nhollas/Jobby/apps/go-api/internal/testenv"
)

func TestCreateBoard(t *testing.T) {
	env := testenv.New(t)
	token := env.Token(t, testenv.TestUserID)

	resp := env.Request(t, http.MethodPost, "/board", token, body{"name": "Job hunt"})
	if resp.StatusCode != http.StatusCreated {
		t.Fatalf("status = %d, want %d", resp.StatusCode, http.StatusCreated)
	}
	created := testenv.DecodeBody[struct {
		Reference string `json:"reference"`
		Name      string `json:"name"`
		Lists     []struct {
			Name string `json:"name"`
		} `json:"lists"`
	}](t, resp)

	if created.Name != "Job hunt" {
		t.Errorf("name = %q, want %q", created.Name, "Job hunt")
	}
	if len(created.Lists) != 5 {
		t.Errorf("got %d lists, want 5", len(created.Lists))
	}

	got := env.Request(t, http.MethodGet, "/board/"+created.Reference, token, nil)
	if got.StatusCode != http.StatusOK {
		t.Errorf("GET created board: status = %d, want %d", got.StatusCode, http.StatusOK)
	}
}

func TestCreateBoardValidation(t *testing.T) {
	env := testenv.New(t)

	resp := env.Request(t, http.MethodPost, "/board", env.Token(t, testenv.TestUserID), body{"name": ""})

	if resp.StatusCode != http.StatusUnprocessableEntity {
		t.Fatalf("status = %d, want %d", resp.StatusCode, http.StatusUnprocessableEntity)
	}
	problem := testenv.DecodeBody[struct {
		Errors map[string]string `json:"errors"`
	}](t, resp)
	if _, ok := problem.Errors["name"]; !ok {
		t.Errorf("no validation error for %q; got %v", "name", problem.Errors)
	}
}

func TestGetBoard(t *testing.T) {
	env := testenv.New(t)
	board := env.SeedBoard(t, testenv.TestUserID, 2)
	token := env.Token(t, testenv.TestUserID)

	created := env.Request(t, http.MethodPost, "/activity", token, validActivity(board.Reference))
	if created.StatusCode != http.StatusCreated {
		t.Fatalf("create activity: status = %d, want %d", created.StatusCode, http.StatusCreated)
	}

	resp := env.Request(t, http.MethodGet, "/board/"+board.Reference, token, nil)
	if resp.StatusCode != http.StatusOK {
		t.Fatalf("status = %d, want %d", resp.StatusCode, http.StatusOK)
	}

	got := testenv.DecodeBody[struct {
		Lists []struct {
			Name     string `json:"name"`
			Position int    `json:"position"`
			Jobs     []struct {
				Reference string `json:"reference"`
			} `json:"jobs"`
		} `json:"lists"`
		Activities []struct {
			Reference string `json:"reference"`
		} `json:"activities"`
	}](t, resp)

	if len(got.Lists) != 5 {
		t.Fatalf("got %d lists, want 5", len(got.Lists))
	}
	if got.Lists[0].Name != "Applied" || got.Lists[0].Position != 1 {
		t.Errorf("first list = %q at position %d, want %q at position 1",
			got.Lists[0].Name, got.Lists[0].Position, "Applied")
	}
	if len(got.Lists[0].Jobs) != 2 {
		t.Errorf("got %d jobs on the first list, want 2", len(got.Lists[0].Jobs))
	}
	if len(got.Activities) != 1 {
		t.Errorf("got %d activities, want 1", len(got.Activities))
	}
}

// TestGetBoardNotFound checks that another user's board is indistinguishable
// from a board that does not exist.
func TestGetBoardNotFound(t *testing.T) {
	env := testenv.New(t)
	token := env.Token(t, testenv.TestUserID)

	tests := []struct {
		name      string
		reference func(t *testing.T) string
	}{
		{
			name:      "unknown board",
			reference: func(*testing.T) string { return "bo_unknown" },
		},
		{
			name: "another user's board",
			reference: func(t *testing.T) string {
				return env.SeedBoard(t, testenv.OtherUserID, 0).Reference
			},
		},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			ref := tt.reference(t)

			resp := env.Request(t, http.MethodGet, "/board/"+ref, token, nil)

			if resp.StatusCode != http.StatusNotFound {
				t.Fatalf("status = %d, want %d", resp.StatusCode, http.StatusNotFound)
			}
			got := testenv.DecodeBody[struct {
				Message string `json:"message"`
			}](t, resp)
			if want := fmt.Sprintf("no resource with reference %q", ref); got.Message != want {
				t.Errorf("message = %q, want %q", got.Message, want)
			}
		})
	}
}
