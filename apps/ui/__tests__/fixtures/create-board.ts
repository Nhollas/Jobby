import { Page, TestFixture, test as base } from "@playwright/test";

// Declare the types of your fixtures.
type CreateBoardFixture = {
  boardId: string;
  createJob: () => Promise<void>;
  cleanUp: () => Promise<void>;
};

export const createBoardFixture = base.extend<CreateBoardFixture>({
  boardId: async ({ page }, use) => {
    // Go to the Boards page
    await page.goto("http://localhost:3000/track/boards");

    // Click the link to open the Create Board Modal
    await page.click('a[href="/track/create-board"]');

    // Wait for the modal to open
    await page.waitForSelector("form");

    const uniqueBoardName = `My New Board From Playwright ${Date.now()}`;

    // Add a response event listener to the page object
    const responsePromise = page.waitForResponse(
      "https://localhost:6001/api/board/create"
    );

    // Fill in the form and submit it
    await page.fill('input[name="name"]', uniqueBoardName);
    await page.click('button[type="submit"]');

    // Wait for the responsePromise to resolve
    const response = await responsePromise;

    const createdBoard = await response.json();

    await use(createdBoard.id);
  },
  cleanUp: async ({ page, boardId }, use) => {
    await use(() => cleanUpBoard({ page, boardId }));
  },
  createJob: async ({ page, boardId }, use) => {
    await use(() => createJobMethod({ page, boardId }));
  },
});

const createJobMethod = async ({
  page,
  boardId,
}: {
  page: Page;
  boardId: string;
}) => {
    // Go to the board page.
    await page.goto(`http://localhost:3000/track/board/${boardId}`);
};

const cleanUpBoard = async ({
  page,
  boardId,
}: {
  page: Page;
  boardId: string;
}) => {
  // Go to the Boards page
  await page.goto("http://localhost:3000/track/boards");

  // Click the "Delete" button for the first board in the list
  await page.click(`a[href="/track/delete-board/${boardId}"]`);

  // click button to confirm delete with text "Delete"
  await page.click('button:has-text("Delete")');

  // Wait for the confirmation dialog to disappear
  await page.waitForSelector('div[role="alertdialog"]', {
    state: "detached",
  });
};
