// Command api serves the Jobby HTTP API.
//
// It is configured by environment variables, which are listed in .env.example.
// It applies any pending database migrations at startup.
package main

import (
	"context"
	"errors"
	"fmt"
	"log/slog"
	"net/http"
	"os"
	"os/signal"
	"strings"
	"syscall"
	"time"

	"github.com/Nhollas/Jobby/apps/go-api/internal/auth"
	"github.com/Nhollas/Jobby/apps/go-api/internal/httpapi"
	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
	"github.com/Nhollas/Jobby/apps/go-api/internal/postgres"
	"github.com/jackc/pgx/v5/pgxpool"
)

func main() {
	if err := run(context.Background()); err != nil {
		fmt.Fprintf(os.Stderr, "api: %v\n", err)
		os.Exit(1)
	}
}

// config is the program's configuration, read from the environment.
type config struct {
	addr              string
	databaseURL       string
	clerkPEM          string
	clerkIssuer       string
	authorizedParties []string
}

// loadConfig reads the configuration from the environment. It reports every
// missing required variable, not just the first.
func loadConfig() (config, error) {
	cfg := config{
		addr:        os.Getenv("ADDR"),
		databaseURL: os.Getenv("DATABASE_URL"),
		// Allow the multi-line PEM to be written on one line with \n escapes.
		clerkPEM:    strings.ReplaceAll(os.Getenv("CLERK_PEM_KEY"), `\n`, "\n"),
		clerkIssuer: os.Getenv("CLERK_ISSUER"),
	}
	if cfg.addr == "" {
		cfg.addr = ":8080"
	}
	for party := range strings.SplitSeq(os.Getenv("CLERK_AUTHORIZED_PARTIES"), ",") {
		if party = strings.TrimSpace(party); party != "" {
			cfg.authorizedParties = append(cfg.authorizedParties, party)
		}
	}

	var errs []error
	if cfg.databaseURL == "" {
		errs = append(errs, errors.New("DATABASE_URL is required"))
	}
	if cfg.clerkPEM == "" {
		errs = append(errs, errors.New("CLERK_PEM_KEY is required"))
	}
	if cfg.clerkIssuer == "" {
		errs = append(errs, errors.New("CLERK_ISSUER is required"))
	}
	return cfg, errors.Join(errs...)
}

// run starts the server and blocks until it fails or ctx is cancelled or the
// process receives SIGINT or SIGTERM, in which case it shuts down gracefully.
func run(ctx context.Context) error {
	ctx, stop := signal.NotifyContext(ctx, os.Interrupt, syscall.SIGTERM)
	defer stop()

	logger := slog.New(slog.NewJSONHandler(os.Stdout, nil))

	cfg, err := loadConfig()
	if err != nil {
		return fmt.Errorf("config: %w", err)
	}

	pool, err := pgxpool.New(ctx, cfg.databaseURL)
	if err != nil {
		return fmt.Errorf("connect to database: %w", err)
	}
	defer pool.Close()

	store := postgres.New(pool)
	if err := store.Migrate(ctx); err != nil {
		return fmt.Errorf("migrate: %w", err)
	}

	verifier, err := auth.NewClerkVerifier(cfg.clerkPEM, cfg.clerkIssuer, cfg.authorizedParties)
	if err != nil {
		return fmt.Errorf("create verifier: %w", err)
	}

	srv := &http.Server{
		Addr:              cfg.addr,
		Handler:           httpapi.NewServer(store, verifier, jobby.SystemClock{}, logger),
		ReadHeaderTimeout: 5 * time.Second,
		ReadTimeout:       15 * time.Second,
		WriteTimeout:      30 * time.Second,
		IdleTimeout:       60 * time.Second,
		// Route net/http's own errors, such as TLS handshake failures, through
		// the structured logger.
		ErrorLog: slog.NewLogLogger(logger.Handler(), slog.LevelError),
	}

	serverErr := make(chan error, 1)
	go func() {
		logger.Info("listening", slog.String("addr", cfg.addr))
		serverErr <- srv.ListenAndServe()
	}()

	select {
	case err := <-serverErr:
		// ListenAndServe only returns once it has failed: Shutdown, the one
		// thing that stops it cleanly, has not been called yet.
		return fmt.Errorf("serve: %w", err)
	case <-ctx.Done():
		// Restore default signal handling, so a second Ctrl-C exits at once.
		stop()
		logger.Info("shutting down")

		shutdownCtx, cancel := context.WithTimeout(context.WithoutCancel(ctx), 15*time.Second)
		defer cancel()
		if err := srv.Shutdown(shutdownCtx); err != nil {
			return fmt.Errorf("shut down: %w", err)
		}
		return nil
	}
}
