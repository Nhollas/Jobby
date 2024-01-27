import { createServer, Server } from 'http'
import { AddressInfo } from 'net'
import { parse } from 'url'
import type { SetupServer } from 'msw/node'
import { test as base } from '@playwright/test'
import { http } from 'msw'
import { setupServer } from 'msw/node'
import next from 'next'

const authFile = 'playwright/.auth/user.json';

export const test = base.extend<
    { http: typeof http },
    {
        port: string
        requestInterceptor: SetupServer
    }
>({
    port: [
        async ({ }, use) => {
            const app = next({ dev: false })
            const handle = app.getRequestHandler()

            await app.prepare()

            const server: Server = await new Promise((resolve) => {
                const server = createServer((req, res) => {
                    const parsedUrl = parse(req.url as string, true)
                    handle(req, res, parsedUrl)
                })

                server.listen((error: any) => {
                    if (error) throw error
                    resolve(server)
                })
            })
            const port = String((server.address() as AddressInfo).port)
            await use(port)
        },
        { scope: 'worker', auto: true },
    ],
    requestInterceptor: [
        async ({ }, use) => {
            const requestInterceptor = setupServer()

            requestInterceptor.listen({
                onUnhandledRequest: 'bypass',
            })

            await use(requestInterceptor)
        },
        { scope: 'worker' },
    ],
    http,
    baseURL: [
        async ({ port }, use) => {
            await use(`http://localhost:${port}`)
        },
        {
            scope: 'test'
        }        
    ],
    page: [
        async ({ baseURL, page, port }, use) => {
            await page.goto(baseURL ?? `http://localhost:${port}`)


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

            await use(page)
        },
        {
            scope: 'test'
        }
    ],
})