import { Job } from "types";
import JobInfo from "./JobInfo";
import { serverApi } from "@/lib/clients/serverApi";

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  const { data: job } = await serverApi.get<Job>(`/job/${jobId}`);

  return <JobInfo job={job} />;
}
