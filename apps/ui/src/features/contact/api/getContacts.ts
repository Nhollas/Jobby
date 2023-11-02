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

interface Props {
  boardRef?: string;
  jobRef?: string;
}

function generateQueryKey({ jobRef, boardRef }: Props) {
  if (jobRef) {
    return ["contacts", { jobRef }];
  } else if (boardRef) {
    return ["contacts", { boardRef }];
  } else {
    return ["contacts"];
  }
}

export const useContactsQuery = ({ jobRef, boardRef }: Props) => {
  return useQuery({
    queryKey: generateQueryKey({ jobRef, boardRef }),
    queryFn: () => getContacts(),
  });
};
