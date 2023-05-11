import { Activities } from "components";
import { getAsync } from "@/lib/serverFetch";
import { Activity } from "types";
import { auth } from "@clerk/nextjs";

// create a delay function to simulate loading for 5 secs.

const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const { getToken } = auth();

  const activities = await getAsync<Activity[]>(`/job/${jobId}/activities`, {
    headers: {
      Authorization: `Bearer ${await getToken()}`,
    },
  });
  await delay(5000);

  return <Activities activities={activities} />;
}
