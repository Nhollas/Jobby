export interface IClient {
  fetch: (url: string, options?: RequestInit) => Promise<Response>
}

type FetchWrapperArgs = {
  baseUrl: string | URL | Request
  defaultConfig?: RequestInit
}

export function fetchWrapper({ baseUrl, defaultConfig }: FetchWrapperArgs) {
  return (url: string, config?: RequestInit) => {
    const mergedConfig = { ...defaultConfig, ...config }
    return fetch(baseUrl + url, mergedConfig)
  }
}
