import { client } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Activity } from "@/types";
import { useMutation } from "@tanstack/react-query";

export async function deleteActivity(activityReference: string) {
  try {
    await client.delete(`/activity/${activityReference}`);
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useDeleteActivity = () => {
  return useMutation({
    onMutate: async (activityReference) => {
      await queryClient.cancelQueries(["boards"]);

      const previousActivities = queryClient.getQueryData<Activity[]>([
        "activities",
      ]);

      queryClient.setQueryData(
        ["activities"],
        previousActivities?.filter(
          (board) => board.reference !== activityReference
        )
      );

      return { previousActivities };
    },
    onSuccess: () => queryClient.invalidateQueries(["activities"]),
    onError: (_, __, context: any) => {
      if (context?.previousActivities) {
        queryClient.setQueryData(["activities"], context.previousActivities);
      }
    },
    mutationFn: deleteActivity,
  });
};
