package postgres

import (
	"context"
	"fmt"
	"time"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
	"github.com/jackc/pgx/v5"
)

// boardWithJobsQuery returns one row per job on a board. An empty list
// produces one row with NULL job columns, and a board with no lists produces
// one row with NULL list and job columns.
const boardWithJobsQuery = `
SELECT b.reference, b.owner_id, b.name, b.created_date, b.last_updated,
       l.reference, l.name, l.position, l.created_date, l.last_updated,
       j.reference, j.company, j.title, j.position, j.created_date, j.last_updated
FROM boards b
LEFT JOIN job_lists l ON l.board_reference = b.reference
LEFT JOIN jobs j      ON j.job_list_reference = l.reference
WHERE b.reference = $1
ORDER BY l.position, j.position`

// BoardWithJobs returns the board with the given reference, with its lists and
// their jobs in position order. It returns a *jobby.NotFoundError if there is
// no such board.
func (s *Store) BoardWithJobs(ctx context.Context, reference string) (*jobby.Board, error) {
	rows, err := s.pool.Query(ctx, boardWithJobsQuery, reference)
	if err != nil {
		return nil, fmt.Errorf("query board %q: %w", reference, err)
	}
	defer rows.Close()

	var board *jobby.Board
	listsByRef := make(map[string]*jobby.JobList)

	for rows.Next() {
		var (
			b                            jobby.Board
			listRef, listName            *string
			listPos                      *int32
			listCreated, listUpdated     *time.Time
			jobRef, jobCompany, jobTitle *string
			jobPos                       *int32
			jobCreated, jobUpdated       *time.Time
		)
		if err := rows.Scan(
			&b.Reference, &b.OwnerID, &b.Name, &b.CreatedDate, &b.LastUpdated,
			&listRef, &listName, &listPos, &listCreated, &listUpdated,
			&jobRef, &jobCompany, &jobTitle, &jobPos, &jobCreated, &jobUpdated,
		); err != nil {
			return nil, fmt.Errorf("scan board %q: %w", reference, err)
		}

		if board == nil {
			board = &b
		}
		if listRef == nil {
			continue
		}

		list, ok := listsByRef[*listRef]
		if !ok {
			list = &jobby.JobList{
				Reference:      *listRef,
				OwnerID:        board.OwnerID,
				BoardReference: board.Reference,
				Name:           *listName,
				Position:       int(*listPos),
				CreatedDate:    *listCreated,
				LastUpdated:    *listUpdated,
			}
			listsByRef[*listRef] = list
			board.Lists = append(board.Lists, list)
		}

		if jobRef == nil {
			continue
		}
		list.Jobs = append(list.Jobs, &jobby.Job{
			Reference:        *jobRef,
			OwnerID:          board.OwnerID,
			BoardReference:   board.Reference,
			JobListReference: list.Reference,
			Company:          *jobCompany,
			Title:            *jobTitle,
			Position:         int(*jobPos),
			CreatedDate:      *jobCreated,
			LastUpdated:      *jobUpdated,
		})
	}
	// Without this check, a connection lost mid-stream would look like the
	// end of the rows.
	if err := rows.Err(); err != nil {
		return nil, fmt.Errorf("read board %q: %w", reference, err)
	}
	if board == nil {
		return nil, &jobby.NotFoundError{Reference: reference}
	}
	return board, nil
}

// InsertBoard stores board, with its lists and their jobs, in one transaction.
func (s *Store) InsertBoard(ctx context.Context, board *jobby.Board) error {
	err := pgx.BeginFunc(ctx, s.pool, func(tx pgx.Tx) error {
		if _, err := tx.Exec(ctx,
			`INSERT INTO boards (reference, owner_id, name, created_date, last_updated)
			 VALUES ($1, $2, $3, $4, $5)`,
			board.Reference, board.OwnerID, board.Name, board.CreatedDate, board.LastUpdated,
		); err != nil {
			return err
		}

		for _, list := range board.Lists {
			if _, err := tx.Exec(ctx,
				`INSERT INTO job_lists (reference, board_reference, owner_id, name, position, created_date, last_updated)
				 VALUES ($1, $2, $3, $4, $5, $6, $7)`,
				list.Reference, board.Reference, list.OwnerID, list.Name, list.Position,
				list.CreatedDate, list.LastUpdated,
			); err != nil {
				return fmt.Errorf("list %q: %w", list.Reference, err)
			}

			for _, job := range list.Jobs {
				if _, err := tx.Exec(ctx,
					`INSERT INTO jobs (reference, job_list_reference, board_reference, owner_id, company, title, position, created_date, last_updated)
					 VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9)`,
					job.Reference, list.Reference, board.Reference, job.OwnerID, job.Company,
					job.Title, job.Position, job.CreatedDate, job.LastUpdated,
				); err != nil {
					return fmt.Errorf("job %q: %w", job.Reference, err)
				}
			}
		}
		return nil
	})
	if err != nil {
		return fmt.Errorf("insert board %q: %w", board.Reference, err)
	}
	return nil
}
