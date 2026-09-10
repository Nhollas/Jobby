package jobby

import (
	"errors"
	"slices"
	"testing"
	"time"
)

var testClock = FixedClock{T: time.Date(2026, 3, 14, 9, 30, 0, 0, time.UTC)}

func TestNewBoardCreatesDefaultLists(t *testing.T) {
	board := NewBoard(testClock, "user_1", "My Board")

	want := []string{"Applied", "Wishlist", "Interview", "Offer", "Rejected"}
	got := make([]string, 0, len(board.Lists))
	for _, list := range board.Lists {
		got = append(got, list.Name)
	}
	if !slices.Equal(got, want) {
		t.Errorf("list names = %v, want %v", got, want)
	}

	for i, list := range board.Lists {
		if list.Position != i+1 {
			t.Errorf("%s position = %d, want %d", list.Name, list.Position, i+1)
		}
		if list.OwnerID != "user_1" {
			t.Errorf("%s OwnerID = %q, want %q", list.Name, list.OwnerID, "user_1")
		}
	}
}

func TestBoardNewActivity(t *testing.T) {
	tests := []struct {
		name         string
		jobReference func(*Board) string
		wantErr      bool
	}{
		{
			name:         "unlinked",
			jobReference: func(*Board) string { return "" },
		},
		{
			name:         "linked to a job on the board",
			jobReference: func(b *Board) string { return b.Lists[0].Jobs[0].Reference },
		},
		{
			name:         "linked to an unknown job",
			jobReference: func(*Board) string { return "jo_unknown" },
			wantErr:      true,
		},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			board := NewBoard(testClock, "user_1", "My Board")
			board.Lists[0].AddJob(testClock, "Acme", "Engineer")

			activity, err := board.NewActivity(testClock, ActivityParams{
				Title:        "Screen",
				Type:         PhoneScreen,
				JobReference: tt.jobReference(board),
			})

			if tt.wantErr {
				var notOnBoard *JobNotOnBoardError
				if !errors.As(err, &notOnBoard) {
					t.Fatalf("NewActivity() error = %v, want *JobNotOnBoardError", err)
				}
				return
			}
			if err != nil {
				t.Fatalf("NewActivity() error = %v", err)
			}
			if activity.OwnerID != "user_1" {
				t.Errorf("OwnerID = %q, want %q", activity.OwnerID, "user_1")
			}
			if activity.BoardReference != board.Reference {
				t.Errorf("BoardReference = %q, want %q", activity.BoardReference, board.Reference)
			}
			if !activity.CreatedDate.Equal(testClock.T) {
				t.Errorf("CreatedDate = %v, want %v", activity.CreatedDate, testClock.T)
			}
		})
	}
}

func TestBoardJobsIteratesInOrder(t *testing.T) {
	board := NewBoard(testClock, "user_1", "My Board")
	board.Lists[0].AddJob(testClock, "Acme", "First")
	board.Lists[0].AddJob(testClock, "Acme", "Second")
	board.Lists[2].AddJob(testClock, "Acme", "Third")

	var titles []string
	for job := range board.Jobs() {
		titles = append(titles, job.Title)
	}

	if want := []string{"First", "Second", "Third"}; !slices.Equal(titles, want) {
		t.Errorf("titles = %v, want %v", titles, want)
	}
}

func TestBoardJobsStopsEarly(t *testing.T) {
	board := NewBoard(testClock, "user_1", "My Board")
	for range 10 {
		board.Lists[0].AddJob(testClock, "Acme", "Job")
	}

	seen := 0
	for range board.Jobs() {
		seen++
		if seen == 3 {
			break
		}
	}
	if seen != 3 {
		t.Errorf("visited %d jobs after breaking at 3", seen)
	}
}

func TestActivityTypeString(t *testing.T) {
	tests := []struct {
		typ  ActivityType
		want string
	}{
		{Apply, "Apply"},
		{OnSiteInterview, "On Site Interview"},
		{ApplicationWithdrawn, "Application Withdrawn"},
		{Other, "Other"},
		{ActivityType(99), "ActivityType(99)"},
		{ActivityType(-1), "ActivityType(-1)"},
	}
	for _, tt := range tests {
		if got := tt.typ.String(); got != tt.want {
			t.Errorf("ActivityType(%d).String() = %q, want %q", int16(tt.typ), got, tt.want)
		}
	}
}

func TestActivityTypeValid(t *testing.T) {
	tests := []struct {
		typ  ActivityType
		want bool
	}{
		{Apply, true},
		{Other, true},
		{Other + 1, false},
		{ActivityType(-1), false},
	}
	for _, tt := range tests {
		if got := tt.typ.Valid(); got != tt.want {
			t.Errorf("ActivityType(%d).Valid() = %v, want %v", int16(tt.typ), got, tt.want)
		}
	}
}

func FuzzReference(f *testing.F) {
	f.Add("bo")
	f.Add("ac")
	f.Add("")

	f.Fuzz(func(t *testing.T, prefix string) {
		ref := newReference(prefix)

		if want := len(prefix) + 1 + referenceLength; len(ref) != want {
			t.Fatalf("len(%q) = %d, want %d", ref, len(ref), want)
		}
		for _, c := range []byte(ref[len(prefix)+1:]) {
			if !slices.Contains([]byte(referenceAlphabet), c) {
				t.Fatalf("newReference(%q) = %q, contains %q, which is not in the alphabet", prefix, ref, c)
			}
		}
		if again := newReference(prefix); again == ref {
			t.Fatalf("newReference(%q) returned %q twice", prefix, ref)
		}
	})
}
