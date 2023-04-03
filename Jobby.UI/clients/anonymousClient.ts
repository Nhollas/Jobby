import axios from "axios";
import https from "https";

const agent = new https.Agent({
  rejectUnauthorized: false,
});

const instance = axios.create({
  baseURL: 'https://localhost:6001/api',
  httpsAgent: agent
});

let options = {};

export const anonymousClient = {
  get: async <R>(url: string): Promise<R | null> => {
    try {
      const { data } = await instance.get<R>(url, options);

      return data;
    } catch (err) {

    }

    return null;
  },
  post: async <B, R = any>(
    url: string,
    body: B
  ): Promise<R> => {
      const { data } = await instance.post<R>(url, body, options);
      return data;
  },
  put: async <B, R = any>(url: string, body: B): Promise<R | null> => {
    try {
      const { data } = await instance.put<R>(url, body, options);
      return data;
    } catch (err) {

    }

    return null;
  },
  delete: async (url: string) => {
    try {
      await instance.delete(url, options);
    } catch (err) {

    }

    return null;
  },
};