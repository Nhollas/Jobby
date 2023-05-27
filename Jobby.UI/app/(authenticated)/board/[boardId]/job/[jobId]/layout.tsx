import { JobNavigation } from "./jobNavigation";

export default async function JobLayout({
  children,
  params: { boardId, jobId },
}: {
  children: React.ReactNode;
  params: { boardId: string; jobId: string };
}) {
  return (
    <div className="w-full">
      <JobNavigation boardId={boardId} jobId={jobId} />
      {children}
    </div>
  );
}
