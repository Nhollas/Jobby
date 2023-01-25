import axios from "axios";
import { getToken } from "next-auth/jwt";
import https from "https";

const agent = new https.Agent({
  rejectUnauthorized: false,
});

const instance = axios.create({
  baseURL: 'https://localhost:6001/api',
  httpsAgent: agent
});

let options = {};

const serverSideHeaders = async (req: any) => {
  const token = await getToken({ req });
  const { accessToken } = token;

  if (token && accessToken) {
    options = {
      ...options,
      headers: {
        authorization: `Bearer ${accessToken}`,
      },
    };
  }
};

export const serverClient = {
  get: async <R>(url: string, req: any): Promise<R> => {
    await serverSideHeaders(req);
    try {
      const { data } = await instance.get<R>(url, options);
      return data;
    } catch (err) {
      console.log(err.response.data.message)
      return err.response.data.message;
    }
  },
  post: async <B, R = any>(
    url: string,
    body: B,
    req: any
  ): Promise<R> => {
    await serverSideHeaders(req);
    try {
      const { data } = await instance.post<R>(url, body, options);
      return data;
    } catch (err) {

    }
  },
  put: async <B, R = any>(url: string, body: B, req: any): Promise<R> => {
    await serverSideHeaders(req);
    try {
      const { data } = await instance.put<R>(url, body, options);
      return data;
    } catch (err) {

    }
  },
  delete: async (url: string, req: any) => {
    await serverSideHeaders(req);
    try {
      await instance.delete(url, options);
    } catch (err) {

    }
  },
};