import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Job } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export type UpdateJobDTO = {
  jobReference: string;
  company: string;
  title: string;
  postUrl: string;
  salary: number;
  city: string;
  colour: string;
  description: string;
  deadline: Date;
};

export async function updateJob(payload: UpdateJobDTO) {
  try {
    const response = await client.put<any, AxiosResponse<Job>, UpdateJobDTO>(
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
      console.error(error);

      if (context?.previousJobs) {
        queryClient.setQueryData(["jobs"], context.previousJobs);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["jobs"]),
    mutationFn: updateJob,
  });
};
