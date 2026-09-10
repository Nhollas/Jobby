# Go best practices, and an audit of `apps/go-api`

> **Status:** this is an independent audit of the port *as first written*. Its findings were applied afterwards. File paths and line numbers below refer to that earlier code; `internal/api` has since been renamed `internal/httpapi`. See the "How this code was checked" section of [`apps/go-api/README.md`](../../apps/go-api/README.md) for what changed and which suggestions were deliberately not adopted.

Research date: 2026-09-10. Toolchain: go1.27.1. Every claim is cited to a primary source. Anything marked **SECONDARY** comes from an individual author, not the Go project or a standards body. Where no primary source could be found, the claim sits in tier C and says so.

Beyond reading the code and the docs, I checked the following directly:

- `go vet ./...` is clean and `gofmt -l .` is clean.
- `go test -race -count=1 ./...` passes, including the testcontainers suite.
- `staticcheck -checks all ./...` reports only ST1000 (package `jobby` has no package comment).
- A scratch program confirmed that `encoding/json` v1 with `DisallowUnknownFields` accepts trailing garbage and case-mismatched keys. It also confirmed that a `ResponseWriter` wrapper without `Unwrap` makes `http.ResponseController.Flush` return "feature not supported".
- I read library source in the module cache for errgroup, golang-jwt, pgx and net/http where the docs were ambiguous.

---

## 1. Summary

**Verdict:** the structure and the error, context and SQL handling are genuinely idiomatic, and the code is mostly safe to learn from. The package layout, consumer-side `Store` interface, `%w` wrapping, `errors.As` mapping, context passing, `rows.Err()`, server timeouts, `MaxBytesReader`, unexported context key and black-box tests are all what the official sources ask for.

It is **not** golden, for three reasons:

- **The commentary is the biggest deviation.** Many comments are tutorial prose comparing Go with C#, and at least three of them are factually wrong. Several of the "Go way" claims are actually one well-known blogger's style (Mat Ryer, SECONDARY), presented as if they were the language's standard.
- **There are a handful of real bugs and spec violations.**
- **The comments are the part most likely to teach the wrong thing,** because they read as authoritative.

In short, the author did not "flavour in" C# habits much. The main habit carried over is using comments as documentation of the migration.

**Top issues, ranked:**

1. **401 for "authenticated but not your board" is the wrong status, and violates an RFC MUST.** RFC 9110 §15.5.2 requires a `WWW-Authenticate` header on every 401, and `fail()` doesn't send one. Semantically this case is 403, or 404 if you want to hide that the board exists (`internal/api/errors.go:39-44`).
2. **Comments state false things about Go.** They claim net/http has no panic backstop, that `HasJob` uses comma-ok, and that embedding `http.ResponseWriter` inherits "every other method". These are tier A doc-comment violations as well, since exported names lack doc comments and `jobby` has no package comment.
3. **`recoverPanic` makes panics *worse* than the default.** The default net/http behaviour already recovers and logs a stack trace. The middleware drops the stack, and it sits outside the logger, so panicking requests are never access-logged (`middleware.go:80-102`, `server.go:51-55`).
4. **`statusRecorder` has no `Unwrap`,** so `http.ResponseController` (Flush, deadlines) stops working behind the logging middleware (`middleware.go:52-60`).
5. **`defer tx.Rollback` inside a loop** in `Migrate` (`postgres/store.go:66`). It happens to be harmless today, but it is the textbook misuse of `defer`. The migrator also has no lock, so two replicas booting together can race.
6. **Request decoding isn't as strict as its comment claims.** It accepts trailing garbage (`{"title":"x"} junk`) and case-insensitive keys, and turns a too-large body (413) into a generic 400 (`json.go:28-41`). It is also a silent behaviour change from the C# API, which ignores unknown fields.
7. **`auth.Verifier` is declared on the producer side,** with one implementation and no fakes. That is the opposite of the "interfaces belong to the consumer" rule the code praises elsewhere (`auth/auth.go:19`).
8. **The package name `api`** is on the Go Code Review Comments list of names to avoid (`internal/api`).
9. **JWT verification doesn't check Clerk's `azp` claim,** which Clerk's docs tell you to validate (`auth/auth.go:40-45`).
10. **Test gaps.** `POST /board` has no test at all, and nothing covers malformed or oversized bodies, the 404 on GET, or panic recovery. One subtest calls `testenv.New`, which truncates the shared database mid-test.

---

## 2. Tier A: clear-cut rules (stated by the official sources, not contested)

| # | Rule | Sources |
|---|---|---|
| A1 | **Every exported name has a doc comment; every package has a package comment.** It is a full sentence starting with the name (packages: "Package x ..."). | go.dev/doc/comment, *Package Comments*, *Types*, *Funcs*: "Every package should have a package comment", "Every exported (capitalized) name should have a doc comment." Code Review Comments, *Doc Comments* and *Comment Sentences*. Google Decisions, *Doc comments*: "All top-level exported names must have doc comments". |
| A2 | **Doc comments are for users of the API, not implementation history.** "Doc comments should not explain internal details such as the algorithm used in the current implementation. Those are best left to comments inside the function body." | go.dev/doc/comment. Go Proverbs: "Documentation is for users." |
| A3 | **Error strings are lowercase with no trailing punctuation,** and ideally identify their origin. | Code Review Comments, *Error Strings*. Google Decisions, *Error strings*. Effective Go, *Errors*. |
| A4 | **Don't discard errors with `_`.** Handle them, return them, or (exceptionally) panic. | Code Review Comments, *Handle Errors*. Google Decisions, *Handle errors*. |
| A5 | **`context.Context` is the first parameter, named `ctx`, and isn't stored in structs.** Context-value keys are an unexported, non-built-in type. | pkg.go.dev/context (package doc and `WithValue`). Code Review Comments, *Contexts*. Google Decisions, *Contexts*. |
| A6 | **Initialisms keep a consistent case** (`ID`, `URL`, `PEM`, `DTO`). | Code Review Comments, *Initialisms*. Google Decisions, *Initialisms*. |
| A7 | **Receiver names are short, consistent abbreviations of the type,** never `this` or `self`. | Code Review Comments, *Receiver Names*. Google Decisions, *Receiver names*. |
| A8 | **No `Get` prefix on getters.** | Effective Go, *Getters*. Google Decisions, *Getters*. |
| A9 | **`defer` runs when the surrounding function returns, not at the end of a block or loop iteration.** | Effective Go, *Defer*: "run immediately before the function executing the defer returns". |
| A10 | **Package names are short, lowercase, single words that say what the package provides.** Avoid `util`, `common`, `misc`, `api`, `types`, `interfaces`. | Effective Go, *Package names*. Go blog, *Package names*, "Bad package names". Code Review Comments, *Package Names*, which lists "api" by name. Google Decisions, *Package names*. |
| A11 | **Use `crypto/rand` for anything security-relevant.** | Google Decisions, *Crypto rand*. |
| A12 | **Don't use panic for normal error handling.** | Code Review Comments, *Don't Panic*. Google Decisions. Go Proverbs. |
| A13 | **Test failures say what was got before what was wanted,** with enough context to identify the input. Use `t.Helper()` in helpers. | Code Review Comments, *Useful Test Failures*. Google Decisions, *Got before want*, *Identify the function*, *Test helpers*. pkg.go.dev/testing `T.Helper`. |
| A14 | **In tests, prefer `t.Context()` over `context.Background()`.** | Google Decisions, *Contexts* (`context.Background()` only in entrypoints, or `(testing.TB).Context()` in tests). pkg.go.dev/testing `T.Context` (Go 1.24). |
| A15 | **Server code lives under `internal/`, with commands under `cmd/`.** | go.dev/doc/modules/layout, *Server project*. |
| A16 | **A 401 response MUST carry `WWW-Authenticate`.** 403 means "authenticated, but those credentials are insufficient". 404 MAY be used to hide a resource's existence. | RFC 9110 §15.5.2, §15.5.4, §15.5.5 (verbatim text quoted in §5.2 below). |
| A17 | **Authentication scheme names are case-insensitive** ("It uses a case-insensitive token to identify the authentication scheme"). | RFC 9110 §11.1. |

## 3. Tier B: strong conventions

| # | Convention | Sources |
|---|---|---|
| B1 | **Interfaces belong in the package that *consumes* them.** Don't define one before there is a real need, and export it only if it is the product. | Code Review Comments, *Interfaces*. Google Decisions, *Interfaces*: "The consumer of the interface should define it". Go Proverbs: "The bigger the interface, the weaker the abstraction." |
| B2 | **Return concrete types from constructors** (`NewX` returns `*X`) unless the type exists only to satisfy an interface. | Effective Go, *Generality*. Code Review Comments, *Interfaces*. |
| B3 | **Wrap with `%w` when callers should inspect the cause. Don't wrap when that would expose implementation details as API.** Put `%w` at the end, don't add "failed to", and don't repeat what the inner error already says. | Go blog, *Working with Errors in Go 1.13*, "Whether to wrap". Google Best Practices, *Error wrapping* and *Adding information to errors*. |
| B4 | **Handle *or* return an error, not both.** Don't log and then return. | Google Best Practices, *Logging errors*: "If you return an error, it's usually better not to log it yourself". |
| B5 | **Give errors structure** (sentinel or typed) and let callers use `errors.Is`/`errors.As`. A custom `Is` method is a documented extension point. | Google Best Practices, *Error structure*. pkg.go.dev/errors. Go 1.13 errors blog. |
| B6 | **Set `http.Server` timeouts.** Shut down gracefully with `Server.Shutdown`, which makes `ListenAndServe` return `ErrServerClosed`. | pkg.go.dev/net/http `Server`, `Server.Shutdown`. |
| B7 | **Bound request bodies with `http.MaxBytesReader`,** which returns `*http.MaxBytesError`. The HTTP status for "too big" is 413. | pkg.go.dev/net/http `MaxBytesReader`. RFC 9110 §15.5.14. |
| B8 | **A `ResponseWriter` wrapper should implement `Unwrap() http.ResponseWriter`** so `http.ResponseController` can reach the underlying writer's Flush and deadline methods. | pkg.go.dev/net/http `NewResponseController`: "The ResponseWriter should be the original value passed to the Handler.ServeHTTP method, or have an Unwrap method returning the original ResponseWriter." |
| B9 | **net/http already recovers handler panics:** "It recovers the panic, logs a stack trace to the server error log, and either closes the network connection or sends an HTTP/2 RST_STREAM". Custom recovery middleware must not lose that information, and should re-panic `http.ErrAbortHandler`. | pkg.go.dev/net/http `Handler`. Source: `net/http/server.go:82-88`. |
| B10 | **Table-driven tests with `t.Run` subtests.** Use `t.Fatal` when continuing is meaningless and `t.Error` otherwise. Black-box `_test` packages test the public surface. | Google Decisions, *Table-driven tests* and *t.Fatal vs t.Error*. pkg.go.dev/testing. |
| B11 | **Test-helper packages are named after the package they support, with a `test` suffix** (`creditcardtest`). The stdlib does the same with `httptest`, `fstest`, `iotest`. | Google Best Practices, *Test doubles and helper packages*. |
| B12 | **A container's iterator method is conventionally named `All()`, returning `iter.Seq`.** | Go blog, *Range Over Function Types*. |
| B13 | **`FromContext` accessors return `(T, bool)`.** | pkg.go.dev/context, the `NewContext`/`FromContext` example. |
| B14 | **`NewX` names a constructor.** Don't give a *type* a `New...` name. | Effective Go, *Constructors and composite literals* (`NewFile`, `ring.New`). This is an inference from convention: no source forbids it outright, which is why it's tier B, not A. |
| B15 | **Verify Clerk session tokens by checking `exp`, `nbf`, and `azp` against your known origins.** | Clerk docs, *Manual JWT verification* (vendor primary source). |

## 4. Tier C: contested, or matters of taste

- **Tutorial comments in learning material.** The official sources don't recognise a "teaching" category; everything in §2 A1/A2 applies. It is still a legitimate choice for a learning repo, as long as you know it isn't idiomatic production Go and the claims are true. What the sources would suggest: a doc comment says what the thing does and why a caller should care. Implementation "why" goes in short in-body comments. History and comparisons ("this replaces AutoMapper") go in the README or a design doc, not in code. They will rot the day `apps/api` is deleted.
- **`defer tx.Rollback(ctx)` with the error ignored.** This technically breaks A4, but pgx documents the pattern explicitly: "a defer tx.Rollback() is safe even if tx.Commit() will be called first" (pgx `Tx.Rollback`). Most Go codebases accept it. Strict linters (errcheck) flag it. Taste, *provided it isn't in a loop*.
- **Ignoring the result of `crypto/rand.Read`.** Since Go 1.24 the docs say: "It never returns an error, and always fills b entirely." So `rand.Read(buf)` without a check is now correct. The comment in `reference.go` is accurate.
- **`DisallowUnknownFields`.** No official Go source recommends or discourages it. Strictness catches typos. Tolerance ("be liberal in what you accept") supports rolling client upgrades. Note that `encoding/json/v2`, still behind `GOEXPERIMENT=jsonv2` in go1.27 (its source carries `//go:build goexperiment.jsonv2`), *ignores* unknown members by default and offers `RejectUnknownMembers` as an opt-in.
- **Error response shape.** RFC 9457 (Problem Details, `application/problem+json`) is a Proposed Standard, and it is optional. The `{message, errors}` shape is fine if it's documented. ASP.NET's `ValidationProblemDetails` follows RFC 9457, but the C# app returns `Results.UnprocessableEntity(result.ValidationErrors)`, so neither app actually uses it.
- **400 vs 422 for "job not on board", and 422 for validation.** RFC 9110 §15.5.21 defines 422 for content that is well-formed but semantically unprocessable. Both codes are defensible.
- **Package named after the project (`jobby`) for the domain.** Not addressed by the Go sources. The "root domain package" style comes from Ben Johnson's *Standard Package Layout* (**SECONDARY**). The official layout doc's example uses `internal/model`, which the Google guide in turn calls uninformative. Neither choice is wrong.
- **`run(ctx) error`, `NewServer` returning an `http.Handler`, `Valid() map[string]string`, `decodeValid[T]`.** These come straight from Mat Ryer's *How I write HTTP services in Go after 13 years* (**SECONDARY**). They are widely liked, but they are one author's style, not Go consensus. The README presents them as "the way Go wants it written"; they are better described as "one respected Go style".
- **Explicit per-row `Scan` vs `pgx.CollectRows`/`RowToStructByName`.** Both are documented pgx usage. Explicit scanning is fine for learning.
- **Concurrent loads with errgroup, costing two pool connections per request.** A performance trade-off, not an idiom question.
- **Calling `stop()` from `signal.NotifyContext` as soon as the context is done,** so a second Ctrl-C force-quits. The `os/signal` example does call `stop()` inside the `ctx.Done()` branch. Not calling it keeps the default kill behaviour disabled until `run` returns. Taste.
- **`http.Server.ErrorLog` bridged to slog** (`slog.NewLogLogger`) so net/http's internal errors come out as structured JSON too. Useful, but no source requires it.
- **`testing.TB` instead of `*testing.T` in `testenv` helpers,** so benchmarks can use them. Nice to have.
- **pgxpool connection check at startup (`pool.Ping`).** I found no primary source saying whether `pgxpool.New` connects eagerly, so I make no claim about it.

---

## 5. Audit of `apps/go-api`

### 5.1 Deviations

Line numbers are as of the audited working tree.

#### Comments

**`internal/jobby/*.go` (package clause)** — Tier A1
- **Problem:** no package comment. staticcheck ST1000 flags it.
- **Source:** go.dev/doc/comment, *Package comments*.
- **Fix:** add `// Package jobby models job-search boards, lists, jobs and activities.` above `package jobby` in one file (for example a new `doc.go`).

**Exported names without doc comments** — Tier A1
- **Where:**
  - `auth/auth.go:14` (`ErrUnauthenticated`), `:24` (`ClerkVerifier`), `:29` (`NewClerkVerifier`), `:49` (`Verify`), `:67` (`WithUser`)
  - `jobby/board.go:20` (`JobList`), `:31` (`Job`), `:45` (`NewBoard`), `:60` (`IsOwnedBy`), `:62` (`AddList`), `:77` (`AddJob`)
  - `jobby/activity.go:49` (`Valid`), `:63` (`Activity`), `:81`
  - `postgres/store.go:15` (`Store`), `:17` (`New`)
  - `postgres/activity.go:15`, `:29`, `:46`
  - `api/server.go:29` (`Server`)
  - `testenv/testenv.go:31-35`, `:40` (`Env`)
- **Problem:** exported identifiers with no doc comment.
- **Source:** go.dev/doc/comment. Code Review Comments, *Doc Comments*.
- **Fix:** one sentence each, starting with the name: `// NewClerkVerifier returns a Verifier that accepts RS256 tokens signed by pemKey and issued by issuer.`

**Doc comments that are migration history instead of API docs** — Tier A2
- **Where:**
  - `api/server.go:13-21`, `:36-38`, `:59-61`
  - `api/errors.go:12-20`
  - `api/json.go:12-15`, `:23-27`
  - `api/activity.go:11-13`, `:25-27`, `:49-55`
  - `api/dto.go:9-13`
  - `api/board.go:45-56`
  - `api/middleware.go:13-15`, `:18`
  - `jobby/clock.go:5-6`, `:17`
  - `jobby/activity.go:8-11`
  - `jobby/board.go:94-98`, `:111-114`, `:124-127`, `:138-139`
  - `jobby/errors.go:38-41`
  - `jobby/reference.go:8-11`, `:16-19`
  - `auth/auth.go:71-74`
  - `postgres/store.go:24-27`, `:81-82`
  - `postgres/board.go:11-15`
  - `testenv/testenv.go:1-3`, `:132`, `:209`
  - the migration SQL header
- **Problem:** the comments describe what C# did ("This is the port of...", "replaces AutoMapper", "Respawn did this"), not what the Go code does for a caller.
- **Source:** go.dev/doc/comment ("should not explain internal details"). Go Proverbs ("Documentation is for users").
- **Fix:** move every C#↔Go comparison into a mapping table. The README's "What happened to each piece of the C# API" table already has most of it, or you could add `docs/api/csharp-to-go.md`. Keep one-line "why" comments that stay true without C# existing, for example "rows.Err() distinguishes a dropped connection from end of rows".

**Comments that are factually wrong**
- **`jobby/board.go:111-114`**
  - **Problem:** the comment says `HasJob` returns "the second return value instead of C#'s out parameter -- the comma-ok idiom", but `HasJob` returns a single `bool`.
  - **Fix:** delete the comment, or write `// HasJob reports whether a job with the given reference is on b.`
- **`api/middleware.go:49-51`**
  - **Problem:** "Embedding the interface means the wrapper inherits every other method for free". Embedding an interface only promotes that interface's three methods. Optional interfaces on the concrete writer (`http.Flusher`, `http.Hijacker`, deadline setters) are *hidden*. I confirmed this with a scratch program: the wrapped writer fails the `http.Flusher` assertion, and `ResponseController.Flush` returns "feature not supported". See also B8.
  - **Fix:** correct the comment and add `Unwrap` (below).
- **`api/middleware.go:80-81`**
  - **Problem:** "Go has no unhandled-exception backstop above this". net/http recovers handler panics and logs a stack trace (B9).
  - **Fix:** reword it. The middleware exists to send a JSON 500 and a structured log line, not to keep the process alive.

#### HTTP status semantics

**`api/errors.go:39-44` returns 401 for `NotOwnedError`** — Tier A16
- **Problem:** the request *has* valid credentials, so 401 is semantically wrong. The response also omits `WWW-Authenticate`, which RFC 9110 says the server MUST send. A 401 also invites clients to refresh the token and retry (§15.5.2: "The user agent MAY repeat the request with a new or replaced Authorization header"), which can never succeed here.
- **Source:** RFC 9110 §15.5.2, §15.5.4, §15.5.5.
- **Fix:** use **404** with the same body as not-found, so references can't be probed, or **403** if you prefer being explicit. See §5.2. If the 401 must stay for frontend compatibility, at least set `WWW-Authenticate: Bearer error="insufficient_scope"` and document the deviation.

#### Panic recovery and the response writer wrapper

**`api/middleware.go:91-96` and `server.go:51-55`** — Tier B9
- **Problem:** the recovered panic is logged without a stack trace, which is worse than net/http's default. Because `recoverPanic` wraps `logRequests`, a panic unwinds through the logger and the request is never access-logged. Recovery also writes a 500 even when the handler already wrote headers, producing a garbled response.
- **Fix:**
  - Log `slog.String("stack", string(debug.Stack()))`.
  - Put `logRequests` *outside* `recoverPanic`, so the 500 is recorded.
  - Only write the 500 if the recorder shows headers weren't sent yet.

**`api/middleware.go:52-60` (`statusRecorder`)** — Tier B8
- **Problem:** there is no `Unwrap`, so `http.ResponseController` can't reach Flush or `SetWriteDeadline` behind it.
- **Fix:** add `func (r *statusRecorder) Unwrap() http.ResponseWriter { return r.ResponseWriter }`. Also record a `wroteHeader` flag.

**`api/middleware.go:22` (Bearer scheme match)** — Tier A17
- **Problem:** `strings.CutPrefix(h, "Bearer ")` is case-sensitive, so a `bearer xxx` header is rejected.
- **Fix:** split on the first space and compare the scheme with `strings.EqualFold(scheme, "Bearer")`.

#### Request decoding

**`api/json.go:37` (strict decoding)** — Tier B (correctness)
- **Problem:** the code comment promises strict decoding, but one `Decode` call doesn't reject trailing data. I verified that `{"name":"a"} garbage` and `{"name":"a"}{"name":"b"}` both decode with a nil error. Keys also match case-insensitively (`encoding/json` `Unmarshal`: "preferring an exact match but also accepting a case-insensitive match"), so `{"TITLE":"x"}` passes `DisallowUnknownFields`.
- **Source:** pkg.go.dev/encoding/json `Decoder.Decode`, `Unmarshal`. Scratch test.
- **Fix:** after decoding, `if err := dec.Decode(&struct{}{}); !errors.Is(err, io.EOF) { return ..., errors.New("request body must contain a single JSON value") }`. Accept the case-insensitivity, or wait for json/v2.

**`api/json.go:31` with `api/activity.go:58-60` and `api/board.go:24-27` (oversized body)** — Tier B7
- **Problem:** the decode error is thrown away and every failure becomes "malformed request body" with a 400. A body over 1 MiB should be a 413.
- **Fix:** `var mbe *http.MaxBytesError; if errors.As(err, &mbe) { status = http.StatusRequestEntityTooLarge }`.

**`api/json.go:35` (`DisallowUnknownFields`)** — Tier C, flagged as a behaviour change
- **Problem:** the C# API ignores unknown JSON properties by default. Microsoft Learn, *Handle unmapped members*: "By default ... they're simply ignored", and `apps/api` sets no `UnmappedMemberHandling`. So the port rejects payloads the C# API accepted, and a frontend that sends an extra field will break.
- **Fix:** make the choice deliberately and document it in the README's "deliberate differences".

#### Package design and API surface

**`api/json.go:16` (`Validator`)** — Tier B1
- **Problem:** an exported interface used only as the constraint of an unexported generic function.
- **Fix:** rename it to `validator`.

**`auth/auth.go:19` (`Verifier`)** — Tier B1
- **Problem:** the interface is declared in the producer package, next to its only implementation. Tests use the real type, as the comment itself says.
- **Fix:** move the interface to `internal/api`, where it's consumed, or have `NewServer` take `*auth.ClerkVerifier`. Either way, `auth` exports concrete types only.

**`internal/api` (package name)** — Tier A10/B
- **Problem:** Code Review Comments names "api" as a meaningless package name. The mitigating factor is that this package is a coherent HTTP transport layer, not a dumping ground.
- **Fix:** `httpapi`, `server`, or `web`. The names `api.Server` and `api.Store` would read better as, for example, `httpapi.Server`.

**`jobby/board.go:128` (`NewActivity`)** — Tier B14
- **Problem:** a *struct type* named `NewActivity` reads as a constructor, not as a parameter bag.
- **Fix:** `ActivityParams` or `ActivityInput`.

**`jobby/board.go:99` (`Jobs() iter.Seq`)** — Tier B12 (weak)
- **Problem:** the conventional name for an iterator method is `All`, but this is a derived view rather than a container's elements.
- **Fix:** `Jobs` is defensible. Optionally add a doc comment noting that it iterates in list-then-position order.

**`auth/auth.go:75` (`UserFromContext`)** — Tier B13
- **Problem:** it returns only a `string`, and the comment asserts the empty case is "unreachable in practice". That invariant lives in another package's middleware.
- **Fix:** `func UserFromContext(ctx) (string, bool)`. Handlers treat `!ok` as a 500, which fails closed.

**`jobby/errors.go:12-16`, `:25`, `:36`, `:52` (sentinels plus custom `Is`)** — Tier B5 (design)
- **Problem:** both sentinels and typed errors exist, but production code only uses `errors.As` on the types (`api/errors.go:35-39`). `ErrNotFound` and `ErrNotOwned` are referenced nowhere, and `ErrConflict` only in one test. `ErrConflict` also maps to **400**, not 409, so the name and the HTTP meaning disagree.
- **Fix:** pick one mechanism. Either keep the types and drop the sentinels and `Is` methods, or rename `ErrConflict` to match its meaning (for example `ErrInvalidReference`).

**`auth/auth.go:14`, `:52` (`ErrUnauthenticated`)** — Tier B3
- **Problem:** the sentinel is never checked by any caller. The jwt error is wrapped with `%w`, which makes golang-jwt's error types part of `auth`'s API (Go 1.13 errors blog: "Do not wrap an error when doing so would expose implementation details").
- **Fix:** either document that `Verify` returns an error wrapping `ErrUnauthenticated` and use `%v` for the jwt cause, or drop the sentinel.

#### Database code

**`postgres/store.go:66` (`defer tx.Rollback(ctx)` inside the `for` loop)** — Tier A9
- **Problem:** the defers pile up until `Migrate` returns. It's harmless today only because each tx is committed or the function returns straight away, and pgx documents Rollback after Commit as safe. It is still the canonical defer-in-a-loop mistake, and it breaks as soon as someone adds a `continue` after `Begin`. The comment justifying it ("the Go equivalent of a using block") is wrong: `using` is block-scoped and `defer` is not.
- **Source:** Effective Go, *Defer*.
- **Fix:** extract `func (s *Store) applyMigration(ctx, name string, body []byte) error` with its own `defer tx.Rollback(ctx)`, or use `pgx.BeginFunc`.

**`postgres/store.go:28-79` (no migration lock)** — Tier C (correctness, no Go-idiom source)
- **Problem:** two instances booting at once both see a migration as unapplied, and one fails.
- **Fix:** `SELECT pg_advisory_lock(<const>)` on a dedicated connection around the loop.

**`postgres/board.go:148` (`return tx.Commit(ctx)`)** — Tier B3 (minor)
- **Problem:** the commit error is unwrapped, unlike every other return in the file.
- **Fix:** `if err := tx.Commit(ctx); err != nil { return fmt.Errorf("commit board %q: %w", board.Reference, err) }`.

#### Error-response writing

**`api/errors.go:72`, `middleware.go:46` (`_ = respond(...)`)** — Tier A4 (minor)
- **Problem:** errors are discarded, while `write()` exists specifically to log them.
- **Fix:** route through `s.write`, or give `badRequest` and `unauthorized` a logger.

#### Concurrency

**`api/board.go:64-78` (errgroup)** — correct, with one caveat
- **Correct:** each variable is written by exactly one goroutine and read after `Wait`, which establishes happens-before, and the race detector passes. The errgroup `ctx` is used only inside the goroutines, and `r.Context()` is used for the user and in `fail()`. First-error-wins cancellation matches errgroup's docs. There is no loop-variable issue.
- **Caveat:** errgroup deliberately does *not* recover or propagate panics (source comment in `errgroup.go`: "It is tempting to propagate panics ... but it creates more problems than it solves"). A panic inside `BoardWithJobs` row assembly kills the whole process, bypassing both `recoverPanic` and net/http's recovery.
- **Fix:** none required. Keep the goroutine bodies trivial, as they are, and know this.

#### JWT verification

**`auth/auth.go:40-45` (parser options)** — Tier B15
- **Correct:** algorithm pinning, issuer and required `exp` are right. golang-jwt checks `nbf` by default (`validator.go`: "We always need to check not-before"). Zero leeway matches the C# `ClockSkew = TimeSpan.Zero`.
- **Problem:** `azp` is not validated, and Clerk says to do that. The C# API didn't either.
- **Fix:** add an allowed-origins config and check `azp` when present.

#### Tests

**`api/activity_test.go:139` (subtest calls `testenv.New`)** — Tier B (test hygiene)
- **Problem:** building a second environment re-runs `Truncate` on the shared database mid-test. It's safe only because the subtests run sequentially and each one seeds its own data. It also blocks `t.Parallel`.
- **Fix:** add `env.ForeignToken(t, userID)`, which signs with a second, locally generated key, instead of building a second environment.

**`api/activity_test.go:219` (`context.Background()`)** — Tier A14
- **Fix:** `t.Context()`.

**`jobby/board_test.go:141-143`** — Tier A13
- **Problem:** the failure message "Valid() disagrees with the name table" doesn't say which input or what value.
- **Fix:** split it into table rows with a message like `ActivityType(%d).Valid() = %v, want %v`.

**Missing coverage** — Tier B10
- **Problem:** there is no test for:
  - `POST /board` at all
  - `GET /board/{ref}` returning 404
  - a malformed body, trailing data, unknown fields, or a body over 1 MiB
  - panic recovery
  - the `WWW-Authenticate` header
  - a lowercase `bearer` scheme
- **Fix:** add table rows. A small `httptest.NewRecorder` unit test for the middleware needs no Docker.

**`testenv/testenv.go:46` (`pool` field)** — Tier C
- **Problem:** the field is stored and never read.
- **Fix:** use it for truncation, which would let `Store.Truncate`, a test-only method on the production type, move into `testenv`. Otherwise remove it.

**`testenv` (package name)** — Tier B11 / C
- **Assessment:** fine. It lives under `internal/`, so it's not importable outside the module, and it's only compiled into test binaries because only `_test.go` files import it. The Go toolchain itself has `internal/testenv`. The Google guide's naming would suggest `apitest`.
- **Fix:** optional rename.

### 5.2 The 401 / 403 / 404 question, directly

RFC 9110, verbatim:

- **§15.5.2 (401):** "indicates that the request has not been applied because it lacks valid authentication credentials for the target resource. The server generating a 401 response MUST send a WWW-Authenticate header field ... If the request included authentication credentials, then the 401 response indicates that authorization has been refused for those credentials. The user agent MAY repeat the request with a new or replaced Authorization header field."
- **§15.5.4 (403):** "If authentication credentials were provided in the request, the server considers them insufficient to grant access. The client SHOULD NOT automatically repeat the request with the same credentials. ... An origin server that wishes to "hide" the current existence of a forbidden target resource MAY instead respond with a status code of 404 (Not Found)."
- **§15.5.5 (404):** "did not find a current representation for the target resource or is not willing to disclose that one exists."

A valid Clerk token asking for someone else's board is exactly the §15.5.4 case. 401 is defensible only by reading its second paragraph loosely, and even then the port breaks the MUST about `WWW-Authenticate`.

**Recommendation:** return **404** with the same body as a missing board. That discloses nothing, and the client won't loop on a token refresh. Mirroring the C# 401 is a legitimate *compatibility* choice, but it carries over a bug, not a Go idiom. If you keep it, say so in the README's "deliberate differences" rather than only in a code comment.

### 5.3 What the port does well (trust these)

- **Layout.** `cmd/api` plus `internal/...` is exactly go.dev/doc/modules/layout's server example.
- **`main` → `run(ctx) error`.** Errors are wrapped with short lowercase context ("config: %w", "connect database: %w") that follows A3/B3. `errors.Join` in `loadConfig` reports every missing variable at once.
- **`http.Server` timeouts are all set.** Graceful shutdown is correct: the buffered `serverErr` channel means no goroutine leak, `ErrServerClosed` is handled, and `context.WithoutCancel(ctx)` correctly derives a live context from the already-cancelled one (pkg.go.dev/context `WithoutCancel`).
- **`api.Store` is declared by its consumer** and holds exactly the four methods used (B1). `jobby.Clock` is one method. `scanRow` is a local one-method interface. Constructors return concrete types (B2).
- **Context.** `ctx` comes first everywhere and is never stored in a struct. The unexported `userKey struct{}` is the documented pattern (A5). `t.Context()` is used in `testenv`.
- **Errors.** Messages are lowercase with no punctuation and no "failed to" (A3). `pgx.ErrNoRows` is translated to a domain error at the boundary. `errors.As` with pointer types is used correctly. `fail()` defaults to 500 with an opaque message and logs only 5xx, which is handle-or-return done properly (B4). The client-disconnect check correctly uses `r.Context().Err()`, not the errgroup context.
- **SQL.** Queries are parameterised (no injection risk), `defer rows.Close()` and `rows.Err()` are always used, and transactions wrap multi-row inserts. The `defer tx.Rollback` in `InsertBoard` is the pgx-documented pattern.
- **HTTP.** Go 1.22 method and wildcard routing with `r.PathValue`. `MaxBytesReader` is used. Responses are encoded to a buffer before `WriteHeader`. Content-Type is set. `http.ErrAbortHandler` is re-panicked. Empty slices marshal as `[]`.
- **Naming.** Initialisms (`OwnerID`, `databaseURL`, `clerkPEM`, `activityDTO`), short consistent receivers (`b`, `l`, `a`, `s`, `v`, `e`) and no `Get` prefixes (`Store.Activity`) all follow A6–A8.
- **Domain.** `ActivityType` has `String()` and `Valid()`. `c&63` over a 64-character alphabet has no modulo bias. `crypto/rand.Read` is used and legitimately unchecked since Go 1.24.
- **Tests.**
  - Table-driven with `t.Run`, got-before-want messages, `t.Helper` in every helper, `t.Fatalf` for preconditions and `t.Errorf` for field checks (A13, B10).
  - Black-box `api_test`, `testing.Short` gating Docker, and `sync.OnceValues` for the shared container.
  - Real RSA keys rather than a mocked verifier.
  - A fuzz test on the reference generator, and an expired-token and wrong-key test.
  - Race-clean.
- **JWT.** Algorithm pinning (`WithValidMethods`), required `exp`, issuer check, `nbf` checked by default.

---

## 6. Sources

**Primary (Go project):**
- https://go.dev/doc/effective_go
- https://go.dev/wiki/CodeReviewComments
- https://go.dev/doc/comment
- https://go.dev/doc/modules/layout
- https://go.dev/blog/package-names
- https://go.dev/blog/go1.13-errors
- https://go.dev/blog/range-functions
- https://go-proverbs.github.io/
- https://pkg.go.dev/net/http (Handler, NewResponseController, MaxBytesReader, Server)
- https://pkg.go.dev/context
- https://pkg.go.dev/testing
- https://pkg.go.dev/encoding/json (Decoder.Decode, DisallowUnknownFields, Unmarshal)
- https://pkg.go.dev/encoding/json/v2
- https://pkg.go.dev/crypto/rand
- https://pkg.go.dev/os/signal#NotifyContext
- https://pkg.go.dev/golang.org/x/sync/errgroup
- Local source, go1.27.1: `net/http/server.go`, `net/http/responsecontroller.go`, `encoding/json/v2/doc.go`

**Primary (Google style guide):**
- https://google.github.io/styleguide/go/decisions
- https://google.github.io/styleguide/go/best-practices

**Primary (libraries and vendors):**
- https://pkg.go.dev/github.com/jackc/pgx/v5 (Tx, BeginFunc, CollectRows). Local source: `tx.go` v5.11.0.
- https://pkg.go.dev/github.com/golang-jwt/jwt/v5. Local source: `validator.go` v5.3.1.
- Local source: `golang.org/x/sync@v0.23.0/errgroup/errgroup.go`
- https://clerk.com/docs/backend-requests/manual-jwt
- https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/missing-members

**Primary (standards):**
- https://www.rfc-editor.org/rfc/rfc9110 (sections 11.1, 15.5.x; quotes taken from the .txt edition)
- https://www.rfc-editor.org/rfc/rfc9457.html

**SECONDARY:**
- Mat Ryer, *How I write HTTP services in Go after 13 years*: https://grafana.com/blog/2024/02/09/how-i-write-http-services-in-go-after-13-years/
- Ben Johnson, *Standard Package Layout*. Mentioned only, not fetched.
