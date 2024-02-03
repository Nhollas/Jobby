import { JobNavigation, JobTitle } from "@/features/job";

export default async function JobLayout({
  children,
  params: { boardReference, jobRef },
}: {
  children: React.ReactNode;
  params: { boardReference: string; jobRef: string };
}) {
  return (
    <div className="w-full">
      <JobTitle jobRef={jobRef} />
      <JobNavigation boardRef={boardReference} jobRef={jobRef} />
      {children}
    </div>
  );
}
