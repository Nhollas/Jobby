import { auth } from "@clerk/nextjs";
import axios, { InternalAxiosRequestConfig } from "axios";
import https from "https";

export const client = axios.create({
  baseURL: "/api/",
  headers: {
    "Content-Type": "application/json",
  },
});

async function authInterceptor(config: InternalAxiosRequestConfig) {
  const token = await auth().getToken();
  
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
}

export const serverClient = axios.create({
  baseURL: "https://localhost:6001/",
  headers: {
    "Content-Type": "application/json",
  },
  httpsAgent: new https.Agent({
    rejectUnauthorized: false,
  }),
});

serverClient.interceptors.request.use(authInterceptor);
