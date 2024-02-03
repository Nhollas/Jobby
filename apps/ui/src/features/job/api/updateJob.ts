import { nextApiClient } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Job } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const UpdateJobSchema = z.object({
  jobReference: z.string(),
  company: z.string(),
  title: z.string(),
  postUrl: z.string(),
  salary: z.number(),
  city: z.string(),
  colour: z.string(),
  description: z.string(),
  deadline: z.date(),
});

export type UpdateJobDTO = z.infer<typeof UpdateJobSchema>;

export async function updateJob(payload: UpdateJobDTO) {
  try {
    const response = await nextApiClient.put<any, AxiosResponse<Job>, UpdateJobDTO>(
      "/job",
      payload
    );
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useUpdateJob = () => {
  return useMutation({
    onMutate: async () => {
      const previousJobs = queryClient.getQueryData<Job[]>(["jobs"]);

      return { previousJobs };
    },
    onError: (error, __, context: any) => {
      if (context?.previousJobs) {
        queryClient.setQueryData(["jobs"], context.previousJobs);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["jobs"]),
    mutationFn: updateJob,
  });
};
