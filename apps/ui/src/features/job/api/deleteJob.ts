import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Job } from "@/types";
import { useMutation } from "@tanstack/react-query";

export async function deleteJob(jobReference: string) {
  try {
    await client.delete(`/job/${jobReference}`);
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useDeleteJob = () => {
  return useMutation({
    onMutate: async (jobReference) => {
      await queryClient.cancelQueries(["jobs"]);

      const previousJobs = queryClient.getQueryData<Job[]>(["jobs"]);

      queryClient.setQueryData(
        ["jobs"],
        previousJobs?.filter((job) => job.reference !== jobReference)
      );

      return { previousJobs };
    },
    onSuccess: () => queryClient.invalidateQueries(["jobs"]),
    onError: (_, __, context: any) => {
      if (context?.previousJobs) {
        queryClient.setQueryData(["jobs"], context.previousJobs);
      }
    },
    mutationFn: deleteJob,
  });
};
