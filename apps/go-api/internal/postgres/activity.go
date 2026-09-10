package postgres

import (
	"context"
	"errors"
	"fmt"

	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
	"github.com/jackc/pgx/v5"
)

// activityColumns lists the activities columns in the order scanActivity reads them.
const activityColumns = `reference, board_reference, job_reference, owner_id,
	title, type, start_date, end_date, note, completed, created_date, last_updated`

// InsertActivity stores a.
func (s *Store) InsertActivity(ctx context.Context, a *jobby.Activity) error {
	_, err := s.pool.Exec(ctx,
		`INSERT INTO activities (`+activityColumns+`)
		 VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, $12)`,
		a.Reference, a.BoardReference, nullable(a.JobReference), a.OwnerID,
		a.Title, int16(a.Type), a.StartDate, a.EndDate, a.Note, a.Completed,
		a.CreatedDate, a.LastUpdated,
	)
	if err != nil {
		return fmt.Errorf("insert activity %q: %w", a.Reference, err)
	}
	return nil
}

// Activity returns the activity with the given reference. It returns a
// *jobby.NotFoundError if there is no such activity.
func (s *Store) Activity(ctx context.Context, reference string) (*jobby.Activity, error) {
	row := s.pool.QueryRow(ctx,
		`SELECT `+activityColumns+` FROM activities WHERE reference = $1`, reference)

	a, err := scanActivity(row)
	if errors.Is(err, pgx.ErrNoRows) {
		return nil, &jobby.NotFoundError{Reference: reference}
	}
	if err != nil {
		return nil, fmt.Errorf("query activity %q: %w", reference, err)
	}
	return a, nil
}

// BoardActivities returns the activities on the given board, ordered by start
// date. It returns an empty slice, not an error, if the board does not exist.
func (s *Store) BoardActivities(ctx context.Context, boardReference string) ([]*jobby.Activity, error) {
	rows, err := s.pool.Query(ctx,
		`SELECT `+activityColumns+` FROM activities WHERE board_reference = $1 ORDER BY start_date`,
		boardReference)
	if err != nil {
		return nil, fmt.Errorf("query activities for board %q: %w", boardReference, err)
	}
	defer rows.Close()

	var activities []*jobby.Activity
	for rows.Next() {
		a, err := scanActivity(rows)
		if err != nil {
			return nil, fmt.Errorf("scan activity for board %q: %w", boardReference, err)
		}
		activities = append(activities, a)
	}
	if err := rows.Err(); err != nil {
		return nil, fmt.Errorf("read activities for board %q: %w", boardReference, err)
	}
	return activities, nil
}

// scanRow is implemented by both pgx.Row and pgx.Rows.
type scanRow interface {
	Scan(dest ...any) error
}

// scanActivity reads one row of activityColumns.
func scanActivity(row scanRow) (*jobby.Activity, error) {
	var (
		a      jobby.Activity
		jobRef *string
		typ    int16
	)
	if err := row.Scan(
		&a.Reference, &a.BoardReference, &jobRef, &a.OwnerID,
		&a.Title, &typ, &a.StartDate, &a.EndDate, &a.Note, &a.Completed,
		&a.CreatedDate, &a.LastUpdated,
	); err != nil {
		return nil, err
	}
	a.JobReference = orEmpty(jobRef)
	a.Type = jobby.ActivityType(typ)
	return &a, nil
}
