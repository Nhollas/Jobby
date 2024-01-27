import { test as setup } from '@playwright/test';

const authFile = 'playwright/.auth/user.json';

setup('authenticate', async ({ page }) => {
  // Perform authentication steps. Replace these actions with your own.
  await page.goto('/sign-in');
  await page.getByLabel("Email address or username").fill('test@test.com');
  await page.getByLabel('Password', { exact: true }).fill('Password123');
  await page.getByRole('button', { name: 'continue' }).click();
  // Wait until the page receives the cookies.
  //
  // Sometimes login flow sets cookies in the process of several redirects.
  // Wait for the final URL to ensure that the cookies are actually set.
  await page.waitForURL('/track/boards');
  // Alternatively, you can wait until the page reaches a state where all cookies are set.
  // End of authentication steps.

  await page.context().storageState({ path: authFile });
});