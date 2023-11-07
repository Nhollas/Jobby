import { client } from "@/lib/clients";
import { Contact } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getJobContacts(jobReference: string) {
  try {
    const response = await client.get<Contact[]>(
      `/job/${jobReference}/contacts`
    );

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useJobContactsQuery = (jobReference: string) => {
  return useQuery({
    queryKey: ["contacts", jobReference],
    queryFn: () => getJobContacts(jobReference),
  });
};
