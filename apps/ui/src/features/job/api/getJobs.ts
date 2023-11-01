import { client } from "@/lib/client";
import { Job } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getJobs() {
  try {
    const response = await client.get<Job[]>("/jobs");

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useJobsQuery = () => {
  return useQuery({
    queryKey: ["jobs"],
    queryFn: getJobs,
  });
};
