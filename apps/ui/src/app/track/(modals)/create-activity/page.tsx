import { ActivityFilter, CreateActivityModal } from "@/features/activity";

export default function Page({
  searchParams,
}: {
  searchParams: { boardRef: string; filter: ActivityFilter; jobRef?: string };
}) {
  const { filter, jobRef, boardRef } = searchParams;

  return (
    <CreateActivityModal boardRef={boardRef} jobRef={jobRef} filter={filter} />
  );
}
