import { client } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Job } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const MoveJobSchema = z.object({
  jobReference: z.string(),
  jobListReference: z.string(),
});

export type MoveJobDTO = z.infer<typeof MoveJobSchema>;

export async function moveJob(payload: MoveJobDTO) {
  try {
    const response = await client.put<any, AxiosResponse<Job>, MoveJobDTO>(
      "/job/list",
      payload
    );
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useMoveJob = () => {
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
    mutationFn: moveJob,
  });
};
