/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  swcMinify: true,
  env: {
    apiHost: 'https://localhost:6001'
  }
}

module.exports = nextConfig
