import { useAuth } from "@clerk/nextjs";
import axios, { AxiosInstance } from "axios";
import https from "https";

const createClient = (getToken: any): AxiosInstance => {
  const instance = axios.create({
    baseURL: "https://localhost:6001/api",
  });

  instance.interceptors.request.use(async (config) => {
    config.headers.Authorization = `Bearer ${await getToken()}`;

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

export function useClientApi() {
  const { getToken } = useAuth();

  return createClient(getToken);
}
