import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
export const authOptions = {
  secret: process.env.NEXTAUTH_SECRET,
  providers: [
    CredentialsProvider({
      name: "Credentials",
      credentials: {
        username: { label: "Username", type: "text", placeholder: "jsmith" },
        password: { label: "Password", type: "password" },
      },
      async authorize(credentials, req) {
        const { username, password } = credentials;

        const https = require("https");
        const agent = new https.Agent({
          rejectUnauthorized: false,
        });

        const options = {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          agent,
          body: JSON.stringify({
            username,
            password,
          }),
        };

        const res = await fetch(
          `https://localhost:6001/api/auth/login`,
          options
        );

        const user = await res.json();

        if (res.ok && user) {
          return user;
        } else return null;
      },
    }),
  ],
  session: {
    strategy: "jwt",
  },
  pages: {
    signIn: "/login",
  },
  callbacks: {
    async jwt({ token, user }) {
      if (user) {
        token.accessToken = user.accessToken;
        token.user = user;
      }
      return token;
    },
    async session({ session, token }) {
      session.user = {
        email: token.user.email,
        name: token.user.name,
      };
      return session;
    },
  },
};
export default NextAuth(authOptions);
