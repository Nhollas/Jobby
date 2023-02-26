import { serverClient } from "../../../../../../clients";
import { Activity } from "../../../../../../types";
import { Activities } from "../../../activities/activities";

async function getActivities(jobId: string) {
  const activities = await serverClient.get<Activity[]>(
    `/job/${jobId}/activities`
  );

  return activities;
}

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const activities = await getActivities(jobId);

  return <Activities activities={activities} />;
}
