import { JobNavigation, JobTitle } from "@/features/job";

export default async function JobLayout({
  children,
  params: { boardRef, jobRef },
}: {
  children: React.ReactNode;
  params: { boardRef: string; jobRef: string };
}) {
  return (
    <div className="w-full">
      <JobTitle jobRef={jobRef} />
      <JobNavigation boardRef={boardRef} jobRef={jobRef} />
      {children}
    </div>
  );
}
