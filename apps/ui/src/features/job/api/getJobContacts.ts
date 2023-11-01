import { client } from "@/lib/client";
import { Activity } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getJobContacts(jobReference: string) {
  try {
    const response = await client.get<Activity[]>(
      `/job/${jobReference}/contacts`
    );

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

type Props = {
  jobReference: string;
};

export const useJobContactsQuery = ({ jobReference }: Props) => {
  return useQuery({
    queryKey: ["contacts", jobReference],
    queryFn: () => getJobContacts(jobReference),
  });
};
