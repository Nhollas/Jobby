import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Activity } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export type CreateActivityDTO = {
  boardReference: string;
  jobReference: string;
  title: string;
  type: number;
  startDate: Date;
  endDate: Date;
  note: string;
  completed: boolean;
};

export async function createActivity(payload: CreateActivityDTO) {
  try {
    const response = await client.post<
      any,
      AxiosResponse<Activity>,
      CreateActivityDTO
    >("/activity", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useCreateActivity = () => {
  return useMutation({
    onMutate: async () => {
      const previousActivities = queryClient.getQueryData<Activity[]>([
        "activities",
      ]);

      return { previousActivities };
    },
    onError: (_, __, context: any) => {
      if (context?.previousActivities) {
        queryClient.setQueryData(["activities"], context.previousActivities);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["activities"]),
    mutationFn: createActivity,
  });
};
