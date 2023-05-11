import { auth } from "@clerk/nextjs";
import { getAsync } from "@/lib/serverFetch";
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

  return <h1 className="p-4">Job info {job.id}</h1>;
}
