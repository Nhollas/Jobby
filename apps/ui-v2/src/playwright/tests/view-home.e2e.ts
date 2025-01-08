import { expect } from "@playwright/test"

import test from "src/playwright/fixtures/next-fixture"

test("We can view our home page", async ({ page }) => {
  await page.goto("/")

  await expect(
    page.getByRole("heading", { name: "Create Nhollas App" }),
  ).toBeVisible()
})
