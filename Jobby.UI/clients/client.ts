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

const clientSideHeaders = async () => {
  if (localStorage.getItem("access_token")) {
    const bearerToken = localStorage.getItem("access_token");

    console.log(bearerToken)

    options = {
      ...options,
      headers: {
        authorization: `Bearer ${bearerToken}`,
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
      return await instance.delete(url, options);
    } catch (err) {

    }
  },
};