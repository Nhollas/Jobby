import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Job } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export type CreateJobDTO = {
  company: string;
  title: string;
  boardReference: string;
  jobListReference: string;
};

export async function createJob(payload: CreateJobDTO) {
  try {
    const response = await client.post<any, AxiosResponse<Job>, CreateJobDTO>(
      "/job",
      payload
    );
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useCreateJob = () => {
  return useMutation({
    onMutate: async () => {
      const previousJobs = queryClient.getQueryData<Job[]>(["jobs"]);

      return { previousJobs };
    },
    onError: (_, __, context: any) => {
      if (context?.previousJobs) {
        queryClient.setQueryData(["jobs"], context.previousJobs);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["jobs"]),
    mutationFn: createJob,
  });
};
