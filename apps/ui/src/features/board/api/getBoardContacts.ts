import { client } from "@/lib/client";
import { Contact } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getBoardContacts(boardReference: string) {
  try {
    const response = await client.get<Contact[]>(
      `/board/${boardReference}/contacts`
    );

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

type Props = {
  boardReference: string;
};

export const useBoardContactsQuery = ({ boardReference }: Props) => {
  return useQuery({
    queryKey: ["contacts", boardReference],
    queryFn: () => getBoardContacts(boardReference),
  });
};