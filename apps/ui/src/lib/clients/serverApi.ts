import axios, { AxiosInstance } from "axios";
import { auth } from "@clerk/nextjs";

import https from "https";

const createAuthorizedInstance = (): AxiosInstance => {
  const instance = axios.create({
    baseURL: "https://localhost:6001/api",
  });

  instance.interceptors.request.use(async (config) => {
    const token = await auth().getToken();

    console.log("token", token);

    config.headers.Authorization = `Bearer ${token}`;

    return config;
  });

  /**
   * Disable only in development mode
   */
  if (process.env.NODE_ENV === "development") {
    const httpsAgent = new https.Agent({
      rejectUnauthorized: false,
    });
    instance.defaults.httpsAgent = httpsAgent;
  }

  return instance;
};

export const serverApi = createAuthorizedInstance();
