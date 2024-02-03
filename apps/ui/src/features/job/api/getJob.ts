import { nextApiClient } from "@/lib/clients";
import { Job } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getJob(jobReference: string) {
  try {
    const response = await nextApiClient.get<Job>(`/job/${jobReference}`);

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useJobQuery = (jobReference: string) => {
  return useQuery({
    queryKey: ["job", jobReference],
    queryFn: () => getJob(jobReference),
  });
};
