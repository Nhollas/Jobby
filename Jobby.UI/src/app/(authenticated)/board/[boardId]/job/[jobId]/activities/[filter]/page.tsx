import { Activity } from "@/types";
import { serverApi } from "@/lib/clients";
import { Activities } from "@/components";

export default async function Page({
  params: { jobId, boardId, filter },
}: {
  params: { jobId: string; boardId: string; filter: string };
}) {
  const { data: activities } = await serverApi.get<Activity[]>(
    `/job/${jobId}/activities`
  );

  return (
    <Activities
      activities={activities}
      boardId={boardId}
      jobId={jobId}
      filter={filter}
    />
  );
}
