import { CreateActivityModal } from "@/components/modals";

export default async function Page({
  searchParams,
}: {
  searchParams: { boardRef: string; filter: string; jobRef?: string };
}) {
  const { filter, jobRef } = searchParams;

  return (
    <CreateActivityModal
      jobs={jobs}
      board={board}
      jobId={jobId}
      filter={filter}
    />
  );
}
