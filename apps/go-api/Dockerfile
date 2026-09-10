# Two stages, and the second one is empty. Because the binary is statically
# linked and the migrations are embedded in it by go:embed, the runtime image
# needs no base OS at all -- no .NET runtime, no glibc, nothing to CVE-scan.
FROM golang:1.27-alpine AS build
WORKDIR /src
COPY go.mod go.sum ./
RUN go mod download
COPY . .
RUN CGO_ENABLED=0 go build -ldflags='-s -w' -o /api ./cmd/api

FROM gcr.io/distroless/static-debian12:nonroot
COPY --from=build /api /api
USER nonroot
EXPOSE 8080
ENTRYPOINT ["/api"]
