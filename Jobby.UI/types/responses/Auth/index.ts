export type ExchangeTokenResponse = {
  bearerToken: string;
  expiresAt: number | null | undefined;
};

export type ExchangeTokenRequest = {
  accessToken?: string;
  refreshToken?: string;
  expiresAt?: number;
};