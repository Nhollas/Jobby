import { AxiosResponse } from "axios";
import { useAuth } from "@clerk/nextjs";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { clientApi } from "@/lib/clients/clientApi";
import { Board, Job } from "@/types";

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
    onSuccess:  async({ data: updatedJob }) => {
      queryClient.setQueryData(
        ["job", updatedJob.id],
        updatedJob
      );

      console.log("updatedJob", updatedJob)

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
  const { getToken } = useAuth();

  async function createJob(values: any) {
    return await clientApi.post<any, AxiosResponse<Job>>("/job/create", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });
  }


  // We simply need to set the query data on the boardId query to include the new job.
  return useMutation(createJob, {
    onSuccess: async ({ data: createdJob }) => {

      console.log("createdJob", createdJob)

      queryClient.setQueryData(["board", createdJob.boardId], (oldBoard: Board | undefined) => {
        console.log("oldBoard", oldBoard)

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

        console.log("updatedJobLists", updatedJobLists)

        return {
          ...oldBoard,
          jobLists: updatedJobLists,
        };
      });
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
