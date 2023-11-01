import { client } from "@/lib/client";
import { Contact } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getContact(contactReference: string) {
  try {
    const response = await client.get<Contact>(`/contact/${contactReference}`);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

type Props = {
  contactReference: string;
};

export const useContactQuery = ({ contactReference }: Props) => {
  return useQuery({
    queryKey: ["contact", contactReference],
    queryFn: () => getContact(contactReference),
  });
};
