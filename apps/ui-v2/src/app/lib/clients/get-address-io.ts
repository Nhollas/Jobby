import { fetchWrapper, IClient } from "."

export const GET_ADDRESS_IO_OTEL_TRACER_NAME = "getAddress.io"

export const GetAddressIOApiClient: IClient = {
  fetch: fetchWrapper({
    baseUrl: "https://api.getAddress.io",
    defaultConfig: {
      headers: {
        "Content-Type": "application/json",
      },
    },
  }),
}
