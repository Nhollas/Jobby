import { serverApi } from "@/lib/clients/serverApi";
import { Job } from "@/types";
import { JobNavigation } from "./JobNavigation";
import JobTitle from "./JobTitle";

export default async function JobLayout({
  children,
  params: { boardId, jobId },
}: {
  children: React.ReactNode;
  params: { boardId: string; jobId: string };
}) {
  const { data: job } = await serverApi.get<Job>(`/job/${jobId}`);

  return (
    <div className="w-full">
      <JobTitle jobId={jobId} initialJob={job} />
      <JobNavigation boardId={boardId} jobId={jobId} />
      {children}
    </div>
  );
}
