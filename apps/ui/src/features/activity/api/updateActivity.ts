import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Activity } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export type UpdateActivityDTO = {
  name: string;
};

export async function updateActivity(payload: UpdateActivityDTO) {
  try {
    const response = await client.put<
      any,
      AxiosResponse<Activity>,
      UpdateActivityDTO
    >("/activity", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useUpdateActivity = () => {
  return useMutation({
    onMutate: async () => {
      const previousActivities = queryClient.getQueryData<Activity[]>([
        "activities",
      ]);

      return { previousActivities };
    },
    onError: (error, __, context: any) => {
      console.error(error);

      if (context?.previousActivities) {
        queryClient.setQueryData(["activities"], context.previousActivities);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["activities"]),
    mutationFn: updateActivity,
  });
};
