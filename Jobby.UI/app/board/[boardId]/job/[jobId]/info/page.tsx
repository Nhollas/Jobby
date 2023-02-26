import { serverClient } from "../../../../../../clients";
import { Job } from "../../../../../../types";

async function getJob(jobId: string) {
  const job = await serverClient.get<Job>(`/job/${jobId}`);

  return job;
}

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const job = await getJob(jobId);

  return <h1>Job info {job.id}</h1>;
}
