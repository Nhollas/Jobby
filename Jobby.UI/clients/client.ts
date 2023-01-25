import axios from "axios";
import https from "https";
import { getSession } from "next-auth/react"

const agent = new https.Agent({
  rejectUnauthorized: false,
});

const instance = axios.create({
  baseURL: 'https://localhost:6001/api',
  httpsAgent: agent
});

let options = {};

const clientSideHeaders = async () => {
  const session = await getSession()
  const { accessToken } = session;

  if (accessToken) {
    options = {
      ...options,
      headers: {
        authorization: `Bearer ${accessToken}`,
      },
    };
  }
};

export const client = {
  get: async <R = any>(url: string): Promise<R> => {
    await clientSideHeaders();
    try {
      const { data } = await instance.get<R>(url, options);
      return data;
    } catch (err) {

    }
  },
  post: async <B, R = any>(
    url: string,
    body: B
  ): Promise<R> => {
    await clientSideHeaders();
    try {
      const { data } = await instance.post<R>(url, body, options);
      return data;
    } catch (err) {
      return err;
    }
  },
  put: async <B, R = any>(url: string, body: B): Promise<R> => {
    await clientSideHeaders();
    try {
      const { data } = await instance.put<R>(url, body, options);
      return data;
    } catch (err) {

    }
  },
  delete: async (url: string) => {
    await clientSideHeaders();
    try {
      await instance.delete(url, options);
    } catch (err) {

    }
  },
};