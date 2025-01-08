import type { NextConfig } from "next"

const nextConfig: NextConfig = {
  /* config options here */
  serverExternalPackages: [
    "@opentelemetry/auto-instrumentations-node",
    "@opentelemetry/sdk-node",
  ],
}

export default nextConfig
