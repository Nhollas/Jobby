import { Activity } from "types";
import { serverApi } from "@/lib/clients/serverApi";

// create a delay function to simulate loading for 5 secs.

const delay = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const { data: activities } = await serverApi.get<Activity[]>(
    `/job/${jobId}/activities`
  );

  // return <Activities activities={activities} />;
  return <div>Activities</div>;
}
