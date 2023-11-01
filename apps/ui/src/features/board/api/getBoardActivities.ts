import { client } from "@/lib/client";
import { Activity } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getBoardActivities(boardReference: string) {
  try {
    const response = await client.get<Activity[]>(
      `/board/${boardReference}/activities`
    );

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

type Props = {
  boardReference: string;
};

export const useBoardActivitiesQuery = ({ boardReference }: Props) => {
  return useQuery({
    queryKey: ["activities", boardReference],
    queryFn: () => getBoardActivities(boardReference),
  });
};
