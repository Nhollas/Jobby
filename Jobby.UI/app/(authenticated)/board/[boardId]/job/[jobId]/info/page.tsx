import { getJob } from "lib/job";

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const job = await getJob(jobId);

  return <h1>Job info {job.id}</h1>;
}
