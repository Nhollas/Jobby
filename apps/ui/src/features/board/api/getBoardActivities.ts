import { nextApiClient } from "@/lib/clients/nextApiClient";
import { Activity } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getBoardActivities(boardReference: string) {
  try {
    const response = await nextApiClient.get<Activity[]>(
      `/board/${boardReference}/activities`
    );

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useBoardActivitiesQuery = (boardReference: string) => {
  return useQuery({
    queryKey: ["activities", boardReference],
    queryFn: () => getBoardActivities(boardReference),
  });
};
