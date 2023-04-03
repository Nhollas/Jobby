import { JobNavigation } from "./jobNavigation";

export default async function JobLayout({
  children,
  params: { boardId, jobId },
}: {
  children: React.ReactNode;
  params: { boardId: string; jobId: string };
}) {
  return (
    <>
      <JobNavigation boardId={boardId} jobId={jobId} />
      {children}
    </>
  );
}
