import axios, { AxiosInstance } from "axios";

import https from "https";

const createAuthorizedInstance = (): AxiosInstance => {
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const instance = axios.create({
    baseURL: "https://localhost:6001/api",
    httpAgent: agent,
  });

  return instance;
};

export const clientApi = createAuthorizedInstance();
