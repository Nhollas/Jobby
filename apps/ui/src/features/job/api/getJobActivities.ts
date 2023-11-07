import { client } from "@/lib/clients";
import { Activity } from "@/types";
import { useQuery } from "@tanstack/react-query";

export async function getJobActivities(jobReference: string) {
  try {
    const response = await client.get<Activity[]>(
      `/job/${jobReference}/activities`
    );

    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

type Props = {
  jobReference: string;
};

export const useJobActivitiesQuery = ({ jobReference }: Props) => {
  return useQuery({
    queryKey: ["activities", jobReference],
    queryFn: () => getJobActivities(jobReference),
  });
};
