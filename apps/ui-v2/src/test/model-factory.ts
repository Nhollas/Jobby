function createMockRequest(overrides?: Partial<Request>): Request {
  const mockedRequest = {
    clone: function (): Request {
      return createMockRequest(overrides)
    },
    ...overrides,
  } as Request satisfies Request

  return mockedRequest
}

const modelFactory = {
  request: createMockRequest,
}

export default modelFactory
