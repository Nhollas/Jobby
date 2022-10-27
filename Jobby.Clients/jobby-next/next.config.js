/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: false,
  swcMinify: true,
  env: {
    APIHOST: 'https://localhost:6001',
    JWT: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGZiZTgxNC03ZmU3LTQ3ZDItOWE1OS1hZWE5OWU5MjUxOTIiLCJ1bmlxdWVfbmFtZSI6InRlc3QiLCJqdGkiOiJlZTU0NTBiMS1lNjE3LTQ2NDktYmNjYi05M2JlN2E3YmRiNDkiLCJleHAiOjE2NjY5NTczODYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjYwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo2MDAxIn0.HTHrMYCRSSRnuXJAy659bUgprKEzJ3PbyB4BGePxiB4'
  }
}

module.exports = nextConfig
