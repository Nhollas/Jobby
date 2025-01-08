import path from "path"

import { defineConfig, devices } from "@playwright/test"
import dotenv from "dotenv"
import { fileURLToPath } from "url"

/**
 * Read environment variables from file.
 * https://github.com/motdotla/dotenv
 */
// Alternatively, read from "../my.env" file.
const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

dotenv.config({ path: path.resolve(__dirname, ".env.local") })

/**
 * See https://playwright.dev/docs/test-configuration.
 */
export default defineConfig({
  testDir: "./src/playwright",
  /* Run tests in files in parallel */
  fullyParallel: true,
  /* Fail the build on CI if you accidentally left test.only in the source code. */
  forbidOnly: !!process.env.CI,
  testMatch: "**/*.e2e.ts",
  /* Retry on CI only */
  retries: undefined,
  /* Opt out of parallel tests on CI. */
  /* Reporter to use. See https://playwright.dev/docs/test-reporters */
  reporter: "html",
  workers: process.env.CI ? 1 : undefined,
  /* Shared settings for all the projects below. See https://playwright.dev/docs/api/class-testoptions. */
  use: {
    trace: "on-first-retry",
  },
  /* Configure projects for major browsers */
  projects: [
    // Setup project
    {
      name: "chromium",
      use: { ...devices["Desktop Chrome"] },
    },
  ],
  /* Run your local dev server before starting the tests */
})
