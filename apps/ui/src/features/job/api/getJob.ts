import { client } from "@/lib/client";
import { Job } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getJob(jobReference: string) {
  try {
    const response = await client.get<Job>(`/job/${jobReference}`);

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

type Props = {
  jobReference: string;
};

export const useJobQuery = ({ jobReference }: Props) => {
  return useQuery({
    queryKey: ["job", jobReference],
    queryFn: () => getJob(jobReference),
  });
};
