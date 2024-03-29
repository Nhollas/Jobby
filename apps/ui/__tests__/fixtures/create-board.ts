import { Board } from "@/types";
import { Page } from "@playwright/test";
import { test } from "./next-fixture";

// Declare the types of your fixtures.
type CreateBoardFixture = {
  board: Board;
  createJob: () => Promise<void>;
  cleanUp: () => Promise<void>;
};

export const createBoardFixture = test.extend<CreateBoardFixture>({
  board: async ({ page }, use) => {
    // Go to the Boards page
    await page.goto("/track/boards");

    // Click the link to open the Create Board Modal
    await page.click('a[href="/track/create-board"]');

    // Wait for the modal to open
    await page.waitForSelector("form");

    const uniqueBoardName = `My New Board From Playwright ${Date.now()}`;

    // Fill in the form and submit it
    await page.fill('input[name="name"]', uniqueBoardName);
    // Add a response event listener to the page object
    const responsePromise = page.waitForResponse(
      "/api/board"
    );

    await page.click('button[type="submit"]');

    // Wait for the responsePromise to resolve
    const response = await responsePromise;

    const createdBoard = (await response.json()) as Board;

    await use(createdBoard);
  },
  cleanUp: async ({ page, board }, use) => {
    await use(() => cleanUpBoard({ page, board }));
  },
  createJob: async ({ page, board }, use) => {
    await use(() => createJobMethod({ page, board }));
  },
});

const createJobMethod = async ({
  page,
  board,
}: {
  page: Page;
  board: Board;
}) => {
  // Go to the board page.
  await page.goto(`/track/board/${board.reference}`);
};

const cleanUpBoard = async ({ page, board }: { page: Page; board: Board }) => {
  // Go to the Boards page
  await page.goto("/track/boards");

  // Click the "Delete" button for the first board in the list
  await page.click(`a[href="/track/delete-board/${board.reference}"]`);

  // click button to confirm delete with text "Delete"
  await page.click('button:has-text("Confirm")');

  // Wait for the confirmation dialog to disappear
  await page.waitForSelector('div[role="alertdialog"]', {
    state: "detached",
  });
};
