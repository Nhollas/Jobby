import { AxiosRequestConfig } from "axios";
import { clientApi } from "lib/clients/clientApi";

export async function getAsync<R>(
  url: string,
  options?: AxiosRequestConfig<any> | undefined
): Promise<R> {
  try {
    const response = await clientApi.get<R>(url, options);

    return response.data;
  } catch (err) {
    console.log(err);
  }
  return null as R;
}

export async function postAsync<B, R = any>(
  url: string,
  body: B,
  options?: AxiosRequestConfig<any> | undefined
): Promise<R | null> {
  try {
    const { data } = await clientApi.post<R>(url, body, options);
    return data;
  } catch (err) {
    console.log(err);
  }

  return null;
}

export async function putAsync<B, R = any>(
  url: string,
  body: B,
  options?: AxiosRequestConfig<any> | undefined
): Promise<R | null> {
  try {
    const { data } = await clientApi.put<R>(url, body, options);
    return data;
  } catch (err) {
    console.log(err);
  }

  return null;
}

export async function deleteAsync(
  url: string,
  options?: AxiosRequestConfig<any> | undefined
) {
  try {
    await clientApi.delete(url, options);
  } catch (err) {
    console.log(err);
  }
}
