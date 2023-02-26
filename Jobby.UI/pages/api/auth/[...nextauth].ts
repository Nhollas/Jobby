import axios from "axios";
import https from "https";
import NextAuth, { AuthOptions } from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";
export const authOptions : AuthOptions = {
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

        const agent = new https.Agent({
          rejectUnauthorized: false,
        });
      
        const instance = axios.create({
          baseURL: "https://localhost:6001/api",
          httpsAgent: agent,
        });
      
        const res = await instance.post(
          `/auth/login`,
          JSON.stringify({
            username,
            password
          }),
          {
            headers: {
              "Content-Type": "application/json",
            },
          }
        );

        if (res.status == 200 && res.data) {
          return res.data;
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
      }
      return token;
    },
    async session({ session, token }) {
      session.accessToken = token.accessToken;

      return session;
    },
  },
};
export default NextAuth(authOptions);
