import { useClientApi } from "@/lib/clients";
import { Activity } from "@/types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

export const useActivitiesQuery = (url: string, initialActivities?: Activity[]) => {
  const clientApi = useClientApi();

  const getActivities = async () => {
    const response = await clientApi.get<Activity[]>(url);

    return response.data;
  };

  return useQuery<Activity[]>({
    queryKey: ["activities"],
    queryFn: getActivities,
    initialData: initialActivities,
    staleTime: Infinity,
    cacheTime: Infinity,
  });
};

export const useUpdateActivity = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();

  async function updateActivity(values: any) {
    await clientApi.put<any, Activity>("/activity/update", values);

    return [values];
  }

  return useMutation(updateActivity, {
    onSuccess: async (data, updatedActivity: Activity) => {
      const queryKeys = [];

      // Global Activities
      queryKeys.push(["/activities"]);

      // Board Activities
      if (updatedActivity.board) {
        queryKeys.push(["/activities", `/board/${updatedActivity.board.id}/activities`]);
      }

      // Job Activities
      if (updatedActivity.job) {
        queryKeys.push(["/activities", `/job/${updatedActivity.job.id}/activities`]);
      }

      // Only invalidate the queries that are affected by the new contact.
      await Promise.all(queryKeys.map((key) => queryClient.invalidateQueries({
        queryKey: key,
        refetchType: "all"
      })));
    },
  });
};