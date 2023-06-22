import { test, expect } from '@playwright/test';

test('Should create a new board and render it on the boards page', async ({ page }) => {
  // Go to the Boards page
  await page.goto('http://localhost:3000/boards');

  // Click the link to open the Create Board Modal
  await page.click('a[href="/create-board"]');

  // Wait for the modal to open
  await page.waitForSelector('form');

  const uniqueBoardName = `My New Board From Playwright ${Date.now()}`;

  // Fill in the form and submit it
  await page.fill('input[name="name"]', uniqueBoardName);
  await page.click('button[type="submit"]');

  // Wait for the modal to close
  await page.waitForSelector('form', { state: 'detached' });

  // Check that the new board was created from the unique name
  const boardName = await page.textContent(`h3:has-text("${uniqueBoardName}")`);
  expect(boardName).toBe(uniqueBoardName);
});