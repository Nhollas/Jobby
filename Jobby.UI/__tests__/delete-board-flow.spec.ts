import { test, expect } from '@playwright/test';

test('should delete a board and remove it from the boards list', async ({ page }) => {
  // Go to the Boards page
  await page.goto('http://localhost:3000/boards');

  // Get the initial list of boards
  const initialBoards = await page.evaluate(() => {
    return Array.from(document.querySelectorAll('.card')).map((card) => {
      const name = card.querySelector('.card-title')?.textContent;
      const createdDate = card.querySelector('.card-description')?.textContent;
      const id = card.getAttribute('data-board-id');
      return { id, name, createdDate };
    });
  });

  // Click the "Delete" button for the first board in the list
  const boardToDelete = initialBoards[0];
  await page.click(`a[href="/delete-board/${boardToDelete.id}"]`);

  // Wait for the confirmation dialog to appear
  await page.waitForSelector('div[role="alertdialog"]');

  // click button to confirm delete with text "Delete"
  await page.click('button:has-text("Delete")');

  // Wait for the confirmation dialog to disappear
  await page.waitForSelector('div[role="alertdialog"]', { state: 'detached' });

  // Get the updated list of boards
  const updatedBoards = await page.evaluate(() => {
    return Array.from(document.querySelectorAll('.card')).map((card) => {
      const name = card.querySelector('.card-title')?.textContent;
      const createdDate = card.querySelector('.card-description')?.textContent;
      const id = card.getAttribute('data-board-id');
      return { id, name, createdDate };
    });
  });

  // Check that the deleted board is no longer in the list
  expect(updatedBoards).not.toContainEqual(boardToDelete);
});