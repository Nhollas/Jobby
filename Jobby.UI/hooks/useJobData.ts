import { AxiosResponse } from "axios";
import { useAuth } from "@clerk/nextjs";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { clientApi } from "@/lib/clients/clientApi";
import { Job } from "@/types";

export const useUpdateJob = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();
  async function updateJob(values: any) {
    return await clientApi.put<any, AxiosResponse<Job>>("/job/update", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });
  }
  return useMutation(updateJob, {
    onSuccess: ({ data: updatedJob }) => {
      console.log("updatedJob", updatedJob);
      queryClient.setQueryData(
        ["job", updatedJob.id],
        updatedJob
      );
    },
  });
};

export const useJobQuery = (jobId: string, initialJob?: Job) => {
  const { getToken } = useAuth();

  const getJob = async () => {
    const response = await clientApi.get<Job>(`/job/${jobId}`, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    return response.data;
  };

  return useQuery<Job>({
    queryKey: ["job", jobId],
    queryFn: getJob,
    initialData: initialJob,
  });
};
