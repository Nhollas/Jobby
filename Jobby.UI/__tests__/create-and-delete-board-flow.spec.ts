import { test, expect } from "@playwright/test";

test.describe("Board tests", () => {
  test("Should create a new board and render it on the boards page. Then be able to delete that same board removing it from the list.", async ({
    page,
  }) => {
    // Go to the Boards page
    await page.goto("http://localhost:3000/track/boards");

    // Click the link to open the Create Board Modal
    await page.click('a[href="/create-board"]');

    // Wait for the modal to open
    await page.waitForSelector("form");

    const uniqueBoardName = `My New Board From Playwright ${Date.now()}`;

    // Fill in the form and submit it
    await page.fill('input[name="name"]', uniqueBoardName);
    await page.click('button[type="submit"]');

    // Wait for the modal to close
    await page.waitForSelector("form", { state: "detached" });

    // Check that the new board was created from the unique name
    const boardName = await page.textContent(
      `h3:has-text("${uniqueBoardName}")`
    );
    
    expect(boardName).toBe(uniqueBoardName);

    // Get the initial list of boards
    const initialBoards = await page.evaluate(() => {
      return Array.from(document.querySelectorAll(".card")).map((card) => {
        const name = card.querySelector(".card-title")?.textContent;
        const createdDate =
          card.querySelector(".card-description")?.textContent;
        const id = card.getAttribute("data-board-id");
        return { id, name, createdDate };
      });
    });

    // Click the "Delete" button for the first board in the list
    const boardToDelete = initialBoards.find(board => board.name === uniqueBoardName);

    await page.click(`a[href="/delete-board/${boardToDelete?.id}"]`);

    // Wait for the confirmation dialog to appear
    await page.waitForSelector('div[role="alertdialog"]');

    // click button to confirm delete with text "Delete"
    await page.click('button:has-text("Delete")');

    // Wait for the confirmation dialog to disappear
    await page.waitForSelector('div[role="alertdialog"]', {
      state: "detached",
    });

    // Get the updated list of boards
    const updatedBoards = await page.evaluate(() => {
      return Array.from(document.querySelectorAll(".card")).map((card) => {
        const name = card.querySelector(".card-title")?.textContent;
        const createdDate =
          card.querySelector(".card-description")?.textContent;
        const id = card.getAttribute("data-board-id");
        return { id, name, createdDate };
      });
    });

    // Check that the deleted board is no longer in the list
    expect(updatedBoards).not.toContainEqual(boardToDelete);
  });
});
