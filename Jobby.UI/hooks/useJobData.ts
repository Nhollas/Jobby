import { AxiosResponse } from "axios";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useClientApi } from "@/lib/clients";
import { Board, Job } from "@/types";
import { CreateJobRequest } from "@/contracts";
import { createJob } from "@/contracts/CreateJob";

export const useUpdateJob = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();
  async function updateJob(values: any) {
    return await clientApi.put<any, AxiosResponse<Job>>("/job/update", values);
  }
  return useMutation(updateJob, {
    onSuccess:  async({ data: updatedJob }) => {
      queryClient.setQueryData(
        ["job", updatedJob.id],
        updatedJob
      );

      // Invalidate the board query this job belongs to.
      await queryClient.invalidateQueries({
        queryKey: ["board", updatedJob.boardId],
        type: 'all'
      });
    },
  });
};

export const useCreateJob = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();
  
  return useMutation({
    mutationFn: (values: CreateJobRequest) => createJob(values, clientApi),
    onSuccess: async ({ data: createdJob }) => {

      queryClient.setQueryData(["board", createdJob.boardId], (oldBoard: Board | undefined) => {
        if (!oldBoard) {
          return oldBoard;
        }

        const updatedJobLists = oldBoard.jobLists.map((jobList) => {
          if (jobList.id === createdJob.jobListId) {
            return {
              ...jobList,
              jobs: [...jobList.jobs, createdJob],
            };
          }
          return jobList;
        });

        return {
          ...oldBoard,
          jobLists: updatedJobLists,
        };
      });
    },
  });
};


export const useJobQuery = (jobId: string, initialJob?: Job) => {
  const clientApi = useClientApi();

  const getJob = async () => {
    const response = await clientApi.get<Job>(`/job/${jobId}`);

    return response.data;
  };

  return useQuery<Job>({
    queryKey: ["job", jobId],
    queryFn: getJob,
    initialData: initialJob,
  });
};
