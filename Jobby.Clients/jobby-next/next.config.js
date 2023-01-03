/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: false,
  swcMinify: true,
  env: {
    APIHOST: "https://localhost:6001",
    JWT: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMzdmMzIyZC0zMGRkLTQ5NTAtYjQwMC0xZjI1OTA3NDE4MGYiLCJ1bmlxdWVfbmFtZSI6IlRlc3QiLCJqdGkiOiJkZDUwMDM2YS0xZGU5LTQ4YzYtODdkNS00MGI1MzVkZWUxMDUiLCJleHAiOjE2Njk1ODE3MjMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjYwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo2MDAxIn0.tELFNdZzikBUplGz__oJcxaHM1_ySzT8N2rbvDMAzSo",
  },
};

module.exports = nextConfig;
