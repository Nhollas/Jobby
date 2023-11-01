import { ActivityFilter, CreateActivityModal } from "@/features/activity";

export default async function Page({
  searchParams,
}: {
  searchParams: { boardRef: string; filter: ActivityFilter; jobRef: string };
}) {
  const { filter, boardRef, jobRef } = searchParams;

  return (
    <CreateActivityModal boardRef={boardRef} jobRef={jobRef} filter={filter} />
  );
}