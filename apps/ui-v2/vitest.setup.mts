import "@testing-library/react"
import "@testing-library/jest-dom/vitest"

import { server } from "@/test/server"

beforeAll(() => server.listen({ onUnhandledRequest: "warn" }))
afterAll(() => server.close())
afterEach(() => server.resetHandlers())
