import { auth } from "@clerk/nextjs";
import { getAsync } from "app/serverClient";
import { Job } from "types";

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const { getToken } = auth();

  const job = await getAsync<Job>(`/job/${jobId}`, {
    headers: {
      Authorization: `Bearer ${await getToken()}`,
    },
  });

  return <h1>Job info {job.id}</h1>;
}
