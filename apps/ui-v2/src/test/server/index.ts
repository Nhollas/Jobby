import { HttpResponseResolver } from "msw"
import { setupServer } from "msw/node"
import isEqual from "lodash.isequal"

import { handlers } from "./handlers"

export const server = setupServer(...handlers)

export function withJsonBody<TExpectedBody>(
  expectedBody: TExpectedBody,
  resolver: HttpResponseResolver,
): HttpResponseResolver {
  return async (args) => {
    const { request } = args

    const contentType = request.headers.get("Content-Type") || ""
    if (!contentType.includes("application/json")) {
      return
    }

    const actualBody = await request.clone().json()

    if (!isEqual(actualBody, expectedBody)) {
      console.error("Request body did not match!", {
        expectedBody,
        actualBody,
      })
      return
    }

    return resolver(args)
  }
}
