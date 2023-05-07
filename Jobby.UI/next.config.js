/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: false,
  swcMinify: true,
  experimental: {
    appDir: true,
    serverActions: true,
    esmExternals: false,
  },
};

module.exports = nextConfig;
