import { nextApiClient } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Job } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const CreateJobSchema = z.object({
  company: z.string(),
  title: z.string(),
  boardReference: z.string(),
  jobListReference: z.string(),
});

export type CreateJobDTO = z.infer<typeof CreateJobSchema>;

export async function createJob(payload: CreateJobDTO) {
  try {
    const response = await nextApiClient.post<any, AxiosResponse<Job>, CreateJobDTO>(
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
