import { getBoardContacts } from "@/features/board";
import { getJobContacts } from "@/features/job";
import { client } from "@/lib/clients";
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
    return ["contacts", jobRef];
  } else if (boardRef) {
    return ["contacts", boardRef];
  } else {
    return ["contacts"];
  }
}

function generateQueryFn({ jobRef, boardRef }: Props) {
  if (jobRef) {
    return () => getJobContacts(jobRef);
  } else if (boardRef) {
    return () => getBoardContacts(boardRef);
  } else {
    return getContacts;
  }
}

export const useContactsQuery = ({ jobRef, boardRef }: Props) => {
  return useQuery({
    queryKey: generateQueryKey({ jobRef, boardRef }),
    queryFn: generateQueryFn({ jobRef, boardRef }),
  });
};
