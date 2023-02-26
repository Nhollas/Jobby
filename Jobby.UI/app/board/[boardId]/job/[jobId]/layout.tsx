import { JobNavigation } from "./jobNavigation";

export default async function JobLayout({
  children,
  params: { boardId, jobId },
}) {
  return (
    <>
      <JobNavigation boardId={boardId} jobId={jobId} />
      {children}
    </>
  );
}
