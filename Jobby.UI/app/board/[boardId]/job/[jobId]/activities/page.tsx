import { getJobActivities } from "lib/job";
import { Activities } from "components";

// create a delay function to simulate loading for 5 secs.

const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const activities = await getJobActivities(jobId);
  await delay(5000);

  return <Activities activities={activities} />;
}
