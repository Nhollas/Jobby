# jobby go-api

One vertical slice of the Jobby API (`apps/api`), written in Go.

It is not a line-by-line translation. The endpoints do the same job and the
domain rules are the same, but the internals use what Go and its standard
library provide instead of recreating the .NET architecture. Where the result
behaves differently from the C# API, it is listed under
[Deliberate differences](#deliberate-differences-from-the-c-api).

## Running it

```bash
make db                          # local Postgres in Docker
cp .env.example .env             # fill in your Clerk issuer and PEM key
set -a && source .env && set +a
make run                         # migrations are applied at startup
```

```bash
make test          # all tests, including real Postgres via testcontainers
make test-short    # unit tests only; no Docker, under a second
make test-race     # all tests under the race detector
make fuzz          # fuzz the reference generator
make lint          # gofmt, go vet, staticcheck
```

## Endpoints

Every endpoint requires a Clerk bearer token. Without a valid one, the response
is `401` with `WWW-Authenticate: Bearer`.

| Method | Path | Responses |
|---|---|---|
| `POST` | `/activity` | `201`; `400` malformed body, or job not on the board; `404` board missing or another user's; `413` body over 1 MiB; `422` validation |
| `POST` | `/board` | `201` with the five default lists; `400`; `413`; `422` |
| `GET` | `/board/{reference}` | `200` with lists, jobs and activities; `404` board missing or another user's |

## Layout

| Package | Contents |
|---|---|
| `cmd/api` | `main`: configuration, wiring, graceful shutdown |
| `internal/jobby` | Domain types and rules. No I/O. |
| `internal/postgres` | `Store`: SQL queries and embedded migrations |
| `internal/auth` | Clerk token verification; the user ID in a context |
| `internal/httpapi` | Routing, handlers, middleware, JSON |
| `internal/testenv` | A Postgres container and running server for integration tests |

Dependencies point inward: `httpapi` and `postgres` import `jobby`, and
`jobby` imports nothing from this module.

## Suggested reading order

1. **`internal/jobby/`**: the domain, and the most direct port of the C#.
2. **`internal/httpapi/activity.go`**: the slice itself. Read it side by side
   with `ActivityEndpoints.cs` and `CreateActivityCommandHandler.cs`. Every
   status the endpoint can return is visible in one function.
3. **`internal/httpapi/errors.go`**: how errors become responses, without
   exceptions or result types.
4. **`internal/httpapi/json.go`**: `decodeValid[T]`, the one place generics
   pay for themselves here.
5. **`internal/postgres/board.go`**: turning joined rows into an object graph.
   This is the part that is more work than EF Core, so judge it fairly.
6. **`internal/httpapi/board.go`**: two queries run concurrently with `errgroup`.
7. **`internal/httpapi/middleware.go`**: middleware as
   `func(http.Handler) http.Handler`, and a `ResponseWriter` wrapper done properly.
8. **The tests.** `internal/jobby/*_test.go` and
   `internal/httpapi/{middleware,json}_test.go` are unit tests that need no
   Docker. `internal/httpapi/{activity,board}_test.go` run against real Postgres.

## For C# developers

### Where each piece of the C# API went

| `apps/api` | Here | Notes |
|---|---|---|
| MediatR, `IDispatcher`, `IRequestHandler` | A method on `*httpapi.Server` | Nothing was ever dispatched dynamically, so the indirection had nothing to do. |
| `IDispatchResult`, seven result types, `ResponseFormattingFilter` | `Server.fail` in `httpapi/errors.go` | Matches error types with `errors.As`. Unrecognised errors become an opaque 500. |
| FluentValidation, `ValidationBehavior` | A `Valid` method on each request type | `decodeValid[T]` calls it. A type without one doesn't compile. |
| `InvalidOperationException` thrown from `Board.AddActivity` | `*jobby.JobNotOnBoardError` returned from `Board.NewActivity` | The caller has to deal with the error where it happens. |
| AutoMapper, `MappingProfileTests` | Hand-written functions in `httpapi/dto.go` | Renaming a field breaks the build, so there is no mapping configuration to test. |
| Ardalis.Specification (`.Include().ThenInclude()`) | `boardWithJobsQuery` in `postgres/board.go` | The SQL is in the file. |
| EF Core, `DbContext`, nine configuration classes, migrations | `internal/postgres` and one `.sql` file | Migrations are compiled into the binary with `go:embed`. |
| `IServiceCollection` | The arguments to `httpapi.NewServer` | Missing wiring is a compile error, not a runtime resolution failure. |
| `IRepository<T>`, published by the persistence layer | `httpapi.Store`, declared by its consumer | Four methods, the ones actually used. `*postgres.Store` satisfies it without importing it. |
| `TimeProvider`, `FakeTimeProvider` | `jobby.Clock`, `jobby.FixedClock` | |
| `IUserService`, `IHttpContextAccessor` | `auth.UserFromContext` | Returns `(string, bool)`. The key type is unexported, so no other package can set it. |
| `ActivityConstants.Types` plus `TypesDictionary` | `jobby.ActivityType` with a `String` method | One list of names instead of two that must be kept in sync. |
| LINQ `SelectMany` | `Board.Jobs`, an `iter.Seq` | `for job := range board.Jobs()` stops as soon as you `break`. |
| `TryGetJobFromBoard(ref, out job)` | `Board.HasJob(ref) bool` | Go returns multiple values instead of `out` parameters. |
| `WebApplicationFactory` | `httptest.NewServer` | `*httpapi.Server` is an `http.Handler`, so it plugs straight in. |
| Respawn | A `TRUNCATE` in `testenv.New` | |
| One xUnit fixture class per scenario | Table-driven tests with `t.Run` | `TestCreateActivity` covers 12 cases in one table. |
| .NET runtime image | `distroless/static` | A statically linked binary of about 10 MB with no OS in the image. |

The C# API is 4 projects, 180 files and 6,379 lines, and uses 31 NuGet
packages. This slice is 6 packages, 24 files and 2,573 lines, 1,034 of them
tests, with 4 direct module dependencies: pgx, golang-jwt, x/sync and
testcontainers. That comparison flatters Go, because the C# API does much more.
Within this slice, though, nearly all the removed code is plumbing, not behaviour.

### Things that work differently from what you'd expect

- **There are no exceptions.** Errors are values that functions return. Most
  of the ceremony you'll see is `if err != nil { return ..., err }`, and that
  is by design: every place a call can fail is visible.
- **Interfaces are satisfied implicitly and declared by their consumer.** You
  don't write `: IStore`. Any type with the right methods fits, so the package
  that needs an interface defines exactly the one it needs.
- **The unit of encapsulation is the package, not the type.** Lowercase names
  are visible throughout their package and nowhere else. There is no
  `private` or `protected`.
- **Zero values mean something.** An empty string, `0`, or `nil` slice is a
  valid value, not an uninitialised one. `Activity.JobReference` is `""` when
  there is no job, rather than a nullable reference.
- **`defer` is scoped to the function, not the block.** It is not `using`. A
  `defer` inside a loop runs when the function returns, not at the end of the
  iteration. The first version of this code got that wrong in `Migrate`.

## Deliberate differences from the C# API

- **Another user's board returns `404`, not `401`.** The request is
  authenticated, so `401` is the wrong status. RFC 9110 also requires a `401` to
  carry `WWW-Authenticate`, which the C# API doesn't send. `404` is permitted in
  place of `403` to hide that the resource exists (RFC 9110 §15.5.4), and the
  body is identical to a genuine not-found.
- **Unknown JSON fields are rejected with `400`; the C# API ignores them.** This
  catches typos in field names, but a client that sends extra fields will break.
  It is a matter of taste, not a Go rule. Field names still match
  case-insensitively, as `encoding/json` always does. `encoding/json/v2` can
  turn that off but is still experimental.
- **Data after the JSON body is rejected, and bodies over 1 MiB get `413`.**
- **`endDate` before `startDate` is a `422`.** The C# validator didn't check it.
- **The token's `azp` claim is checked when `CLERK_AUTHORIZED_PARTIES` is set**,
  as Clerk recommends. The C# API doesn't check it.
- **There is no `Guid` surrogate key.** The C# schema stored both a `Guid Id`
  and a `string Reference` on every table but only ever queried by reference,
  so here the reference is the primary key.
- **References are drawn from a 64-character alphabet, not 62,** so every
  character is equally likely.
- **Linking an activity to a job on a different board has a test.** The C# code
  handled this correctly but didn't test it.
- **"Job not on board" stays `400`, matching the C# API.** `422` would also be
  defensible.
- **Only three endpoints exist.** Contacts, jobs, lists, updates and deletes are
  not ported. Porting them is the exercise.

## How this code was checked

The first version was written by an AI assistant. Then a separate research pass
audited it against primary sources: Effective Go, Go Code Review Comments,
Google's Go style guide, go.dev/doc/comment, standard library docs, and
RFC 9110. That audit is in
[`docs/api/go-best-practices.md`](../../docs/api/go-best-practices.md). It ranks
each practice as a clear-cut rule, a strong convention, or a matter of taste,
and it found real problems, which have since been fixed:

- **Documentation.** Many exported names had no doc comment, and three comments
  were factually wrong. Much of the commentary compared Go with C#, and that
  material has moved into this README.
- **Panics.** The panic middleware dropped the stack trace, which made it worse
  than net/http's built-in recovery. The logging wrapper also broke
  `http.ResponseController`.
- **Status codes.** Another user's board returned `401` (see above), and the
  Bearer scheme was matched case-sensitively.
- **Decoding.** Trailing data was accepted, and oversized bodies got `400`
  instead of `413`.
- **Migrations.** A `defer` sat inside the loop in `Migrate`, and there was no
  lock against concurrent migrations.
- **Package design.** An interface was declared on the producer side, an
  unexported-only interface was exported, and the package was named `api`, a
  name Go Code Review Comments lists as one to avoid.
- **Tests.** There were coverage gaps, and one test rebuilt the environment
  partway through.

Current state:

- `gofmt` and `go vet` are clean.
- `staticcheck -checks all` reports nothing.
- `go test -race ./...` passes: 21 test functions, and the unit tier runs in
  under a second without Docker.
- `golangci-lint` with `errcheck`, `revive`, `bodyclose`, `contextcheck`,
  `errorlint`, `noctx` and `gocritic` enabled reports 11 findings. Each has been
  checked and left as it is:
  - `bodyclose` ×9: false positives. Response bodies are closed by
    `t.Cleanup` in `testenv.RequestRaw`, which the linter can't see.
  - `contextcheck` ×1: a false positive. The panic handler uses `r.Context()`.
  - `errorlint` ×1: deliberate. `auth.Verify` formats the jwt error with `%v`
    because wrapping it would make golang-jwt's error types part of this
    package's API ([Go 1.13 errors, "Whether to wrap"](https://go.dev/blog/go1.13-errors)).

Audit suggestions that were deliberately not adopted:

- **Rejecting unknown JSON fields, and `400` for "job not on board".** Both are
  matters of taste, and both are documented above.
- **One transaction per migration.** Instead, all pending migrations run in one
  transaction under `pg_advisory_xact_lock`. The lock is released automatically
  and a failure leaves the schema untouched. The trade-off is that pending
  migrations commit together.
- **RFC 9457 problem-details responses.** That standard is optional, and the
  `{"message": ...}` shape is documented here.
- **Renaming `testenv` or `Board.Jobs`.** The Go toolchain has its own
  `internal/testenv`, and `Jobs` is a derived view of the board rather than its
  elements, so the conventional `All` name fits less well.

Some patterns here, such as `run(ctx) error`, the `Valid` method, and
`decodeValid`, come from Mat Ryer's
[*How I write HTTP services in Go after 13 years*](https://grafana.com/blog/2024/02/09/how-i-write-http-services-in-go-after-13-years/).
It is widely admired, but it is one experienced author's style, not an official
Go convention.

## What is not here yet

- **OpenAPI.** Go tools usually generate code *from* a spec
  ([`oapi-codegen`](https://github.com/oapi-codegen/oapi-codegen)), which is the
  opposite direction from Swashbuckle.
- **Tracing and metrics** (OpenTelemetry).
- **A CI workflow.**
- **Down migrations.** The migrator only applies embedded files in order.
- **Re-reading after create.** `POST /board` returns the board it built in
  memory rather than reading it back from the database.
