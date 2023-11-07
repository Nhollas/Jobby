import { client } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Activity } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const CreateActivitySchema = z.object({
  boardReference: z.string(),
  jobReference: z.string(),
  title: z.string(),
  type: z.number(),
  startDate: z.date(),
  endDate: z.date(),
  note: z.string(),
  completed: z.boolean(),
});

export type CreateActivityDTO = z.infer<typeof CreateActivitySchema>;

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
