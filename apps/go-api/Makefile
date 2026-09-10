.PHONY: help run test test-short test-race lint fuzz build tidy db db-down

help: ## Show this help
	@grep -hE '^[a-z-]+:.*?## ' $(MAKEFILE_LIST) | awk 'BEGIN{FS=":.*?## "};{printf "  \033[36m%-12s\033[0m %s\n", $$1, $$2}'

run: ## Run the API (needs a database + Clerk env vars)
	go run ./cmd/api

test: ## Full suite, including the Postgres container tests
	go test ./...

test-short: ## Domain + unit tests only. No Docker, runs in under a second.
	go test -short ./...

test-race: ## Full suite under the race detector
	go test -race ./...

fuzz: ## Fuzz the reference generator for 30s
	go test ./internal/jobby -run Fuzz -fuzz FuzzReference -fuzztime 30s

lint: ## gofmt check, go vet, and staticcheck
	@test -z "$$(gofmt -l .)" || (echo "unformatted:"; gofmt -l .; exit 1)
	go vet ./...
	go run honnef.co/go/tools/cmd/staticcheck@v0.8.1 -checks all ./...

build: ## Build a static binary into ./bin
	CGO_ENABLED=0 go build -ldflags='-s -w' -o bin/api ./cmd/api

tidy: ## Sync go.mod/go.sum
	go mod tidy

db: ## Start a local Postgres for `make run`
	docker run -d --name jobby-pg -p 5432:5432 \
		-e POSTGRES_USER=jobby -e POSTGRES_PASSWORD=jobby -e POSTGRES_DB=jobby \
		postgres:17-alpine

db-down: ## Remove the local Postgres
	docker rm -f jobby-pg
