import { authOptions } from "../pages/api/auth/[...nextauth]";
import axios from "axios";
import https from "https";
import { getServerSession } from "next-auth";

const agent = new https.Agent({
  rejectUnauthorized: false,
});

const instance = axios.create({
  baseURL: 'https://localhost:6001/api',
  httpsAgent: agent
});

let options = {};

const serverSideHeaders = async () => {
  const session = await getServerSession(authOptions);

  if (session && session.bearerToken ) {
    options = {
      ...options,
      headers: {
        authorization: `Bearer ${session.bearerToken}`,
      },
    };
  }
};

export const serverClient = {
  get: async <R>(url: string): Promise<R> => {
    await serverSideHeaders();
    try {
      const { data } = await instance.get<R>(url, options);

      return data;
    } catch (err) {
    }
    return null as R;
  },
  post: async <B, R = any>(
    url: string,
    body: B
  ): Promise<R | null> => {
    await serverSideHeaders();
    try {
      const { data } = await instance.post<R>(url, body, options);
      return data;
    } catch (err) {
    }

    return null;
  },
  put: async <B, R = any>(url: string, body: B): Promise<R | null> => {
    await serverSideHeaders();
    try {
      const { data } = await instance.put<R>(url, body, options);
      return data;
    } catch (err) {

    }

    return null;
  },
  delete: async (url: string) => {
    await serverSideHeaders();
    try {
      await instance.delete(url, options);
    } catch (err) {

    }

    return null;
  },
};