import { Board } from "@/types";
import { Page, test as base } from "@playwright/test";

// Declare the types of your fixtures.
type CreateBoardFixture = {
  boardRef: string;
  createJob: () => Promise<void>;
  cleanUp: () => Promise<void>;
};

export const createBoardFixture = base.extend<CreateBoardFixture>({
  boardRef: async ({ page }, use) => {
    // Go to the Boards page
    await page.goto("http://localhost:3000/track/boards");

    // Click the link to open the Create Board Modal
    await page.click('a[href="/track/create-board"]');

    // Wait for the modal to open
    await page.waitForSelector("form");

    const uniqueBoardName = `My New Board From Playwright ${Date.now()}`;

    // Fill in the form and submit it
    await page.fill('input[name="name"]', uniqueBoardName);
    // Add a response event listener to the page object
    const responsePromise = page.waitForResponse(
      "http://localhost:3000/api/board"
    );

    await page.click('button[type="submit"]');

    // Wait for the responsePromise to resolve
    const response = await responsePromise;

    const createdBoard = (await response.json()) as Board;

    await use(createdBoard.reference);
  },
  cleanUp: async ({ page, boardRef }, use) => {
    await use(() => cleanUpBoard({ page, boardRef }));
  },
  createJob: async ({ page, boardRef }, use) => {
    await use(() => createJobMethod({ page, boardRef }));
  },
});

const createJobMethod = async ({
  page,
  boardRef,
}: {
  page: Page;
  boardRef: string;
}) => {
  // Go to the board page.
  await page.goto(`http://localhost:3000/track/board/${boardRef}`);
};

const cleanUpBoard = async ({
  page,
  boardRef,
}: {
  page: Page;
  boardRef: string;
}) => {
  // Go to the Boards page
  await page.goto("http://localhost:3000/track/boards");

  // Click the "Delete" button for the first board in the list
  await page.click(`a[href="/track/delete-board/${boardRef}"]`);

  // click button to confirm delete with text "Delete"
  await page.click('button:has-text("Delete")');

  // Wait for the confirmation dialog to disappear
  await page.waitForSelector('div[role="alertdialog"]', {
    state: "detached",
  });
};
