import NextAuth, { AuthOptions } from "next-auth";
import GitHubProvider from "next-auth/providers/github";
import GoogleProvider from "next-auth/providers/google";
import exchangeToken from 'lib/auth/exchangeToken';

export const authOptions : AuthOptions = {
  providers: [
    GitHubProvider({
      clientId: process.env.GITHUB_CLIENT_ID!,
      clientSecret: process.env.GITHUB_CLIENT_SECRET!,
    }),
    GoogleProvider({
      clientId: process.env.GOOGLE_CLIENT_ID!,
      clientSecret: process.env.GOOGLE_CLIENT_SECRET!,
      authorization: { params: { access_type: "offline", prompt: "consent" } },
    })
  ],
  secret: process.env.NEXTAUTH_SECRET!,
  callbacks: {
    async jwt({ token, account }) {
      if (account) {
        const response = await exchangeToken(
          account.provider, 
          account.access_token, 
          account.refresh_token, 
          account.expires_at
        );

        return {
          ...token,
          accessToken: account.access_token,
          refreshToken: account.refresh_token,
          expiresAt: account.expires_at,
          provider: account.provider,
          bearerToken: response.bearerToken,
        }

      } else if (Date.now() < token.expiresAt * 1000) {
        // If the access token has not expired yet, return it
        return token
      } else {
        // If the access token has expired, refresh it
        const response = await exchangeToken(
          token.provider, 
          token.accessToken, 
          token.refreshToken, 
          token.expiresAt
        );

        return {
          ...token,
          bearerToken: response.bearerToken,
          expiresAt: response.expiresAt,
        }
      }
    },
    async session({ session, token, user }) {
      session.bearerToken = token.bearerToken;
      return session;
    },
  },
};


export default NextAuth(authOptions);
