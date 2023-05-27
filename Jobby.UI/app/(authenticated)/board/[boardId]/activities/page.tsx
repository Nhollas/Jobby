import { Activity } from "types";
import { serverApi } from "@/lib/clients/serverApi";

export async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const { data: activities } = await serverApi.get<Activity[]>(
    `/board/${boardId}/activities`
  );

  // return <Activities activities={activities} />;
  return <div>Activities</div>;
}

export default Page;
