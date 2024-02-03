import { nextApiClient } from "@/lib/clients/nextApiClient";
import { Contact } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getContact(contactReference: string) {
  try {
    const response = await nextApiClient.get<Contact>(
      `/contact/${contactReference}`
    );
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useContactQuery = (contactReference: string) => {
  return useQuery({
    queryKey: ["contacts", contactReference],
    queryFn: () => getContact(contactReference),
  });
};
