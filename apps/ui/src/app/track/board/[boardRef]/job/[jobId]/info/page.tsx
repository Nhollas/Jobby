import JobInfo from "./JobInfo";

export default async function Page({
  params: { jobId },
}: {
  params: { jobId: string };
}) {
  return <JobInfo jobId={jobId} />;
}
