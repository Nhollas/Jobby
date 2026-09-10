// Package testenv runs the API against a real PostgreSQL database for
// integration tests. It requires Docker, and skips tests run with -short.
package testenv

import (
	"bytes"
	"context"
	"crypto/rand"
	"crypto/rsa"
	"crypto/x509"
	"encoding/json"
	"encoding/pem"
	"io"
	"log/slog"
	"net/http"
	"net/http/httptest"
	"sync"
	"testing"
	"time"

	"github.com/Nhollas/Jobby/apps/go-api/internal/auth"
	"github.com/Nhollas/Jobby/apps/go-api/internal/httpapi"
	"github.com/Nhollas/Jobby/apps/go-api/internal/jobby"
	"github.com/Nhollas/Jobby/apps/go-api/internal/postgres"
	"github.com/golang-jwt/jwt/v5"
	"github.com/jackc/pgx/v5/pgxpool"
	tcpostgres "github.com/testcontainers/testcontainers-go/modules/postgres"
)

// Users that tests act as.
const (
	TestUserID  = "user_test_owner"
	OtherUserID = "user_someone_else"
)

const (
	testIssuer = "https://test.clerk.example"
	// testParty is the only authorized party the test server accepts.
	testParty = "http://localhost:3000"
)

// Now is the time reported by the server's clock.
var Now = time.Date(2026, 3, 14, 9, 30, 0, 0, time.UTC)

// Env is a running API server and the store behind it.
type Env struct {
	Store  *postgres.Store
	Server *httptest.Server

	signingKey *rsa.PrivateKey
}

// container is the PostgreSQL container shared by every test in the process.
var container = sync.OnceValues(func() (*tcpostgres.PostgresContainer, error) {
	return tcpostgres.Run(context.Background(),
		"postgres:17-alpine",
		tcpostgres.WithDatabase("jobby"),
		tcpostgres.WithUsername("jobby"),
		tcpostgres.WithPassword("jobby"),
		tcpostgres.BasicWaitStrategies(),
	)
})

// New returns an Env with an empty, migrated database. Everything it starts is
// stopped when t ends.
func New(t *testing.T) *Env {
	t.Helper()
	if testing.Short() {
		t.Skip("skipping integration test in short mode")
	}
	ctx := t.Context()

	pg, err := container()
	if err != nil {
		t.Fatalf("start postgres container: %v", err)
	}
	dsn, err := pg.ConnectionString(ctx, "sslmode=disable")
	if err != nil {
		t.Fatalf("get connection string: %v", err)
	}
	pool, err := pgxpool.New(ctx, dsn)
	if err != nil {
		t.Fatalf("connect to postgres: %v", err)
	}
	t.Cleanup(pool.Close)

	store := postgres.New(pool)
	if err := store.Migrate(ctx); err != nil {
		t.Fatalf("migrate: %v", err)
	}
	// Truncate at the start rather than the end, so that a failed test leaves
	// its rows behind to inspect.
	if _, err := pool.Exec(ctx, `TRUNCATE boards, job_lists, jobs, activities CASCADE`); err != nil {
		t.Fatalf("truncate: %v", err)
	}

	signingKey := generateKey(t)
	verifier, err := auth.NewClerkVerifier(publicKeyPEM(t, signingKey), testIssuer, []string{testParty})
	if err != nil {
		t.Fatalf("create verifier: %v", err)
	}

	logger := slog.New(slog.NewTextHandler(io.Discard, nil))
	server := httptest.NewServer(httpapi.NewServer(store, verifier, jobby.FixedClock{T: Now}, logger))
	t.Cleanup(server.Close)

	return &Env{Store: store, Server: server, signingKey: signingKey}
}

// Token returns a valid bearer token for userID.
func (e *Env) Token(t *testing.T, userID string) string {
	t.Helper()
	return sign(t, e.signingKey, claims(userID, testParty, time.Now().Add(time.Hour)))
}

// ExpiredToken returns a token for userID that is valid except that it expired
// an hour ago.
func (e *Env) ExpiredToken(t *testing.T, userID string) string {
	t.Helper()
	return sign(t, e.signingKey, claims(userID, testParty, time.Now().Add(-time.Hour)))
}

// ForeignToken returns a token for userID that is valid except that it is
// signed by a key the server does not trust.
func (e *Env) ForeignToken(t *testing.T, userID string) string {
	t.Helper()
	return sign(t, generateKey(t), claims(userID, testParty, time.Now().Add(time.Hour)))
}

// TokenForParty returns a token for userID that is valid except that its azp
// claim is party.
func (e *Env) TokenForParty(t *testing.T, userID, party string) string {
	t.Helper()
	return sign(t, e.signingKey, claims(userID, party, time.Now().Add(time.Hour)))
}

// claims returns Clerk-shaped token claims. Expiry is checked against the real
// time, not the server's Clock, so it must be relative to time.Now.
func claims(userID, party string, expires time.Time) jwt.MapClaims {
	return jwt.MapClaims{
		"sub": userID,
		"iss": testIssuer,
		"azp": party,
		"exp": expires.Unix(),
		"iat": time.Now().Unix(),
	}
}

func sign(t *testing.T, key *rsa.PrivateKey, c jwt.MapClaims) string {
	t.Helper()
	token, err := jwt.NewWithClaims(jwt.SigningMethodRS256, c).SignedString(key)
	if err != nil {
		t.Fatalf("sign token: %v", err)
	}
	return token
}

func generateKey(t *testing.T) *rsa.PrivateKey {
	t.Helper()
	key, err := rsa.GenerateKey(rand.Reader, 2048)
	if err != nil {
		t.Fatalf("generate RSA key: %v", err)
	}
	return key
}

// publicKeyPEM encodes key's public half as a PKIX PEM block, the format
// Clerk publishes.
func publicKeyPEM(t *testing.T, key *rsa.PrivateKey) string {
	t.Helper()
	der, err := x509.MarshalPKIXPublicKey(&key.PublicKey)
	if err != nil {
		t.Fatalf("marshal public key: %v", err)
	}
	return string(pem.EncodeToMemory(&pem.Block{Type: "PUBLIC KEY", Bytes: der}))
}

// Request sends body, encoded as JSON, to path. A nil body sends no body, and
// an empty token sends no Authorization header. The response body is closed
// when t ends.
func (e *Env) Request(t *testing.T, method, path, token string, body any) *http.Response {
	t.Helper()
	var raw []byte
	if body != nil {
		var err error
		if raw, err = json.Marshal(body); err != nil {
			t.Fatalf("encode request body: %v", err)
		}
	}
	return e.RequestRaw(t, method, path, token, raw)
}

// RequestRaw is like Request but sends body as is.
func (e *Env) RequestRaw(t *testing.T, method, path, token string, body []byte) *http.Response {
	t.Helper()
	var r io.Reader
	if body != nil {
		r = bytes.NewReader(body)
	}
	req, err := http.NewRequestWithContext(t.Context(), method, e.Server.URL+path, r)
	if err != nil {
		t.Fatalf("create request: %v", err)
	}
	req.Header.Set("Content-Type", "application/json")
	if token != "" {
		req.Header.Set("Authorization", "Bearer "+token)
	}

	resp, err := e.Server.Client().Do(req)
	if err != nil {
		t.Fatalf("%s %s: %v", method, path, err)
	}
	t.Cleanup(func() {
		if err := resp.Body.Close(); err != nil {
			t.Errorf("close response body: %v", err)
		}
	})
	return resp
}

// DecodeBody decodes resp's JSON body into a T.
func DecodeBody[T any](t *testing.T, resp *http.Response) T {
	t.Helper()
	var v T
	if err := json.NewDecoder(resp.Body).Decode(&v); err != nil {
		t.Fatalf("decode %T from response: %v", v, err)
	}
	return v
}

// SeedBoard stores and returns a board owned by ownerID, with jobCount jobs on
// its first list.
func (e *Env) SeedBoard(t *testing.T, ownerID string, jobCount int) *jobby.Board {
	t.Helper()
	clock := jobby.FixedClock{T: Now}
	board := jobby.NewBoard(clock, ownerID, "Test Board")
	for range jobCount {
		board.Lists[0].AddJob(clock, "Test Company", "Test Job")
	}
	if err := e.Store.InsertBoard(t.Context(), board); err != nil {
		t.Fatalf("seed board: %v", err)
	}
	return board
}
