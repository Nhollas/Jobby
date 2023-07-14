// src/mocks/handlers.js
import { rest } from 'msw'

export const handlers = [
  rest.get('http://localhost:3000/api/mock', (req, res, ctx) => {
    // Persist user's authentication in the session
    return res(
      // Respond with a 200 status code
      ctx.status(200),
        // And a response body, if necessary
      ctx.json({
        message: 'Mocked Hello World',
      }),
    )
  }),
]