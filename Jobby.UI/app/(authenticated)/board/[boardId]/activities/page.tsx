import { serverClient } from "clients";
import { Activity } from "types";
import { Activities } from "components/Activities";

async function getActivities(boardId: string) {
  const activities = await serverClient.get<Activity[]>(
    `/board/${boardId}/activities`
  );

  return activities;
}

export async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const activities = await getActivities(boardId);

  return <Activities activities={activities} />;
}

export default Page;
