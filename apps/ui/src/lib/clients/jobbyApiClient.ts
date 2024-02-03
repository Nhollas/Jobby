import { auth } from "@clerk/nextjs";
import axios, { InternalAxiosRequestConfig } from "axios";
import { env } from "../env";
import https from "https";

async function authInterceptor(config: InternalAxiosRequestConfig) {
  const token = await auth().getToken();

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
}

export const jobbyApiClient = axios.create({
  baseURL: env().JOBBY_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
  httpsAgent: new https.Agent({
    rejectUnauthorized: false,
  }),
});

jobbyApiClient.interceptors.request.use(authInterceptor);
