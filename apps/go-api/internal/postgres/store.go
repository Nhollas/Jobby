// Package postgres stores jobby domain objects in PostgreSQL.
package postgres

import (
	"context"
	"embed"
	"fmt"
	"io/fs"
	"slices"

	"github.com/jackc/pgx/v5"
	"github.com/jackc/pgx/v5/pgxpool"
)

// Store reads and writes jobby domain objects. It is safe for concurrent use.
type Store struct {
	pool *pgxpool.Pool
}

// New returns a Store that uses pool.
func New(pool *pgxpool.Pool) *Store {
	return &Store{pool: pool}
}

//go:embed migrations/*.sql
var migrationsFS embed.FS

// migrationLockID identifies the advisory lock that serializes Migrate across
// processes. Its value is arbitrary but must never change.
const migrationLockID = 7_210_553_118

// Migrate applies, in filename order, each embedded migration that is not yet
// recorded in the schema_migrations table. All pending migrations run in one
// transaction while holding an advisory lock, so it is safe for several
// processes to call Migrate at once, and a failing migration leaves the schema
// unchanged.
func (s *Store) Migrate(ctx context.Context) error {
	names, err := fs.Glob(migrationsFS, "migrations/*.sql")
	if err != nil {
		return fmt.Errorf("list migrations: %w", err)
	}
	slices.Sort(names)

	return pgx.BeginFunc(ctx, s.pool, func(tx pgx.Tx) error {
		// The lock is released automatically when the transaction ends.
		if _, err := tx.Exec(ctx, `SELECT pg_advisory_xact_lock($1)`, migrationLockID); err != nil {
			return fmt.Errorf("lock migrations: %w", err)
		}
		if _, err := tx.Exec(ctx, `
			CREATE TABLE IF NOT EXISTS schema_migrations (
				name       TEXT PRIMARY KEY,
				applied_at TIMESTAMPTZ NOT NULL DEFAULT now()
			)`); err != nil {
			return fmt.Errorf("create schema_migrations: %w", err)
		}
		for _, name := range names {
			if err := applyMigration(ctx, tx, name); err != nil {
				return err
			}
		}
		return nil
	})
}

// applyMigration runs the named migration in tx and records it, unless it has
// already been applied.
func applyMigration(ctx context.Context, tx pgx.Tx, name string) error {
	var applied bool
	if err := tx.QueryRow(ctx,
		`SELECT EXISTS (SELECT 1 FROM schema_migrations WHERE name = $1)`, name,
	).Scan(&applied); err != nil {
		return fmt.Errorf("check migration %s: %w", name, err)
	}
	if applied {
		return nil
	}

	body, err := migrationsFS.ReadFile(name)
	if err != nil {
		return fmt.Errorf("read migration %s: %w", name, err)
	}
	if _, err := tx.Exec(ctx, string(body)); err != nil {
		return fmt.Errorf("apply migration %s: %w", name, err)
	}
	if _, err := tx.Exec(ctx, `INSERT INTO schema_migrations (name) VALUES ($1)`, name); err != nil {
		return fmt.Errorf("record migration %s: %w", name, err)
	}
	return nil
}

// nullable returns nil for the empty string, so that it is stored as NULL.
func nullable(s string) *string {
	if s == "" {
		return nil
	}
	return &s
}

// orEmpty returns the empty string for a NULL column.
func orEmpty(s *string) string {
	if s == nil {
		return ""
	}
	return *s
}
