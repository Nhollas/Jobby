/* eslint-disable react-hooks/rules-of-hooks */
/* eslint-disable no-empty-pattern */
import { test as base, Route } from "@playwright/test"

import { server } from "@/test/server"
import { SetupServer } from "msw/node"
import { setupNextServer } from "../setup"
import { buildLocalUrl, createTestUtils } from "../utils"

export const test = base.extend<
  {
    utils: ReturnType<typeof createTestUtils>
    po: ReturnType<typeof createTestUtils>["po"]
    serverRequestInterceptor: SetupServer
    interceptBrowserRequest: (
      url: string | RegExp,
      options: Parameters<Route["fulfill"]>[0],
    ) => Promise<void>
  },
  {
    port: string
  }
>({
  baseURL: async ({ port }, use) => {
    await use(buildLocalUrl(port))
  },
  utils: async ({ page }, use) => {
    const u = createTestUtils({ page })

    await use(u)
  },
  po: async ({ utils }, use) => {
    await use(utils.po)
  },
  port: [
    async ({}, use) => {
      const port = await setupNextServer()

      await use(port)
    },
    { auto: true, scope: "worker" },
  ],
  serverRequestInterceptor: [
    async ({}, use) => {
      server.listen({ onUnhandledRequest: "bypass" })

      await use(server)

      server.close()
    },
    {
      scope: "test",
    },
  ],
  interceptBrowserRequest: [
    async ({ page }, use) => {
      async function interceptBrowserRequest(
        url: string | RegExp,
        options: Parameters<Route["fulfill"]>[0],
      ) {
        await page.route(url, async (route) => {
          await route.fulfill(options)
        })
      }

      await use(interceptBrowserRequest)
    },
    { scope: "test" },
  ],
})

export default test
