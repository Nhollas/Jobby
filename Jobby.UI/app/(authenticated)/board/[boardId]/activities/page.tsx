import { Activity } from "types";
import { serverApi } from "@/lib/clients";
import { Activities } from "@/components";

export async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const { data: activities } = await serverApi.get<Activity[]>(
    `/board/${boardId}/activities`
  );

  return <Activities activities={activities} boardId={boardId} />;
}

export default Page;
