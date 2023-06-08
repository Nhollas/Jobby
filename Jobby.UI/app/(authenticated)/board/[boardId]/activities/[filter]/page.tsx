import { Activity } from "types";
import { serverApi } from "@/lib/clients";
import { Activities } from "@/components";

export async function Page({
  params: { boardId, filter },
}: {
  params: { boardId: string; filter: string };
}) {
  const { data: activities } = await serverApi.get<Activity[]>(
    `/board/${boardId}/activities`
  );

  return (
    <Activities activities={activities} filter={filter} boardId={boardId} />
  );
}

export default Page;
