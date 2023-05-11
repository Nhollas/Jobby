"use server";

import axios, { AxiosInstance } from "axios";
import { auth } from "@clerk/nextjs";

import https from "https";

const createAuthorizedInstance = (): AxiosInstance => {
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const instance = axios.create({
    baseURL: "https://localhost:6001/api",
    httpAgent: agent,
  });

  instance.interceptors.request.use(async (config) => {
    const token = await auth().getToken();

    config.headers.Authorization = `Bearer ${token}`;

    return config;
  });

  return instance;
};

export const serverApi = createAuthorizedInstance();
