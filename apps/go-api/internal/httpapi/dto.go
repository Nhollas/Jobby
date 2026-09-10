package httpapi

import (
	"time"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
)

// activityDTO is the JSON representation of a jobby.Activity.
type activityDTO struct {
	Reference      string    `json:"reference"`
	Title          string    `json:"title"`
	Name           string    `json:"name"`
	Type           int16     `json:"type"`
	StartDate      time.Time `json:"startDate"`
	EndDate        time.Time `json:"endDate"`
	Note           string    `json:"note"`
	Completed      bool      `json:"completed"`
	BoardReference string    `json:"boardReference"`
	JobReference   string    `json:"jobReference,omitempty"`
	CreatedDate    time.Time `json:"createdDate"`
	LastUpdated    time.Time `json:"lastUpdated"`
}

func toActivityDTO(a *jobby.Activity) activityDTO {
	return activityDTO{
		Reference:      a.Reference,
		Title:          a.Title,
		Name:           a.Type.String(),
		Type:           int16(a.Type),
		StartDate:      a.StartDate,
		EndDate:        a.EndDate,
		Note:           a.Note,
		Completed:      a.Completed,
		BoardReference: a.BoardReference,
		JobReference:   a.JobReference,
		CreatedDate:    a.CreatedDate,
		LastUpdated:    a.LastUpdated,
	}
}

// boardDTO is the JSON representation of a jobby.Board and its activities.
type boardDTO struct {
	Reference   string        `json:"reference"`
	Name        string        `json:"name"`
	Lists       []jobListDTO  `json:"lists"`
	Activities  []activityDTO `json:"activities"`
	CreatedDate time.Time     `json:"createdDate"`
	LastUpdated time.Time     `json:"lastUpdated"`
}

// jobListDTO is the JSON representation of a jobby.JobList.
type jobListDTO struct {
	Reference string   `json:"reference"`
	Name      string   `json:"name"`
	Position  int      `json:"position"`
	Jobs      []jobDTO `json:"jobs"`
}

// jobDTO is the JSON representation of a jobby.Job.
type jobDTO struct {
	Reference string `json:"reference"`
	Company   string `json:"company"`
	Title     string `json:"title"`
	Position  int    `json:"position"`
}

func toBoardDTO(board *jobby.Board, activities []*jobby.Activity) boardDTO {
	// Every slice is made non-nil, so that an empty one encodes as [] rather
	// than null.
	lists := make([]jobListDTO, 0, len(board.Lists))
	for _, list := range board.Lists {
		jobs := make([]jobDTO, 0, len(list.Jobs))
		for _, job := range list.Jobs {
			jobs = append(jobs, jobDTO{
				Reference: job.Reference,
				Company:   job.Company,
				Title:     job.Title,
				Position:  job.Position,
			})
		}
		lists = append(lists, jobListDTO{
			Reference: list.Reference,
			Name:      list.Name,
			Position:  list.Position,
			Jobs:      jobs,
		})
	}

	activityDTOs := make([]activityDTO, 0, len(activities))
	for _, a := range activities {
		activityDTOs = append(activityDTOs, toActivityDTO(a))
	}

	return boardDTO{
		Reference:   board.Reference,
		Name:        board.Name,
		Lists:       lists,
		Activities:  activityDTOs,
		CreatedDate: board.CreatedDate,
		LastUpdated: board.LastUpdated,
	}
}
