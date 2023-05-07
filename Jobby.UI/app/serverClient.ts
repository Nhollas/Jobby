"use server";

import axios, { AxiosRequestConfig } from "axios";
import https from "https";

const agent = new https.Agent({
  rejectUnauthorized: false,
});

const instance = axios.create({
  baseURL: "https://localhost:6001/api",
  httpsAgent: agent,
});

// const setToken = async () => {
//   const { getToken } = auth();

//   const token = await getToken();

//   console.log(token);
// };

export async function getAsync<R>(
  url: string,
  options?: AxiosRequestConfig<any> | undefined
): Promise<R> {
  try {
    const response = await instance.get<R>(url, options);

    return response.data;
  } catch (err) {}
  return null as R;
}

export async function postAsync<B, R = any>(
  url: string,
  body: B,
  options?: AxiosRequestConfig<any> | undefined
): Promise<R | null> {
  try {
    const { data } = await instance.post<R>(url, body, options);
    return data;
  } catch (err) {}

  return null;
}

export async function putAsync<B, R = any>(
  url: string,
  body: B,
  options?: AxiosRequestConfig<any> | undefined
): Promise<R | null> {
  try {
    const { data } = await instance.put<R>(url, body, options);
    return data;
  } catch (err) {}

  return null;
}

export async function deleteAsync(
  url: string,
  options?: AxiosRequestConfig<any> | undefined
) {
  try {
    await instance.delete(url, options);
  } catch (err) {}
}
