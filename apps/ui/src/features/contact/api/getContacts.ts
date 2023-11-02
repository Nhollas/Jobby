import { client } from "@/lib/client";
import { Contact } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getContacts() {
  try {
    const response = await client.get<Contact[]>("/contacts");
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useContactsQuery = () => {
  return useQuery({
    queryKey: ["contacts"],
    queryFn: () => getContacts(),
  });
};
