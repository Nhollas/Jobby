import { JobInfo } from "@/features/job";

export default async function Page({
  params: { jobRef },
}: {
  params: { jobRef: string };
}) {
  return <JobInfo jobRef={jobRef} />;
}
