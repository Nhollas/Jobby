import { anonymousClient } from "clients/anonymousClient";
import { ExchangeTokenRequest, ExchangeTokenResponse } from "types/responses/Auth";

async function exchangeToken(provider: string | undefined, accessToken: string | undefined, refreshToken: string | undefined, expiresAt: number | undefined): Promise<ExchangeTokenResponse> {
  const response = await anonymousClient.post<ExchangeTokenRequest, ExchangeTokenResponse>(`/auth/exchange-token/${provider}`, {
    accessToken,
    refreshToken,
    expiresAt,
  });

  return {
    bearerToken: response.bearerToken,
    expiresAt: response.expiresAt ?? expiresAt,
  };
}

export default exchangeToken;