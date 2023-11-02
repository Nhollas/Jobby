import { test } from "@playwright/test";
import { createBoardFixture } from "./fixtures/create-board";

test.describe("Create Contact Link Directs To Correct Route.", () => {
  test("From /track/contacts Page should direct user to /track/create-contact", async ({
    page,
  }) => {
    await page.goto("http://localhost:3000/track/contacts");

    // Find the Link with text "Create Contact"
    const createContactLink = await page.$("a:has-text('Create Contact')");

    // Check if the link was found before clicking it
    if (createContactLink) {
      await createContactLink.click();

      // Wait for the page to navigate before getting the URL
      await page.waitForURL("http://localhost:3000/track/create-contact");
    } else {
      throw new Error("Create contact link could not be found on page.");
    }
  });

  createBoardFixture(
    "From /track/board/:ref/contacts Page should direct user to /track/create-contact?boardRef:ref",
    async ({ page, board, cleanUp }) => {
      const { reference } = board;

      await page.goto(
        `http://localhost:3000/track/board/${reference}/contacts`
      );

      // Find the Link with text "Create Contact" and query param boardId
      const createContactLink = await page.$(
        `a[href="/track/create-contact?boardRef=${reference}"]`
      );

      // Check if the link was found before clicking it
      if (createContactLink) {
        await createContactLink.click();

        // Wait for the page to navigate before getting the URL
        await page.waitForURL(
          `http://localhost:3000/track/create-contact?boardRef=${reference}`
        );
      } else {
        throw new Error(
          "Create contact link with query param boardId could not be found on page."
        );
      }

      await cleanUp();
    }
  );
});
